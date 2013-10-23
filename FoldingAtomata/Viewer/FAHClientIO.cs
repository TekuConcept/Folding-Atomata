using FoldingAtomata.NTrajectory;
using FoldingAtomata.PyON;
using FoldingAtomata.Sockets;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NViewer
{
    public class FAHClientIO
    {
        public FAHClientIO(ClientSocket socket)
        {
            _socket = socket;
            ConnectToFAHClient();
            Authenticate();
        }
        public List<int> GetSlotIDs()
        {
            Console.Write("Determining available slots... found { ");

            _socket.Send("slot-info\n");
            string BEGIN = "\"id\":", END = ",\n";
            string slotInfoStr = ReadResponse();

            List<int> slotIDs = new List<int>();
            int index = slotInfoStr.IndexOf(BEGIN, 0);
            while (index != -1)
            {
                var value = StringManip.Between(slotInfoStr, BEGIN, END, index);
                value = StringManip.Trim(value, " \"");

                int id;
                id = int.Parse(value);
                slotIDs.Add(id);

                index = slotInfoStr.IndexOf(BEGIN, index + 1);
                Console.Write(id + " ");
            }

            Console.WriteLine("}");

            return slotIDs;
        }
        public List<Trajectory> GetTrajectories()
        {
            List<Trajectory> trajectories = new List<Trajectory>();
            var slotIDs = GetSlotIDs();
            foreach (int id in slotIDs)
            {
                Console.Write("Downloading trajectory for slot {0}... ", id);
                Console.Clear();

                string trajectoryRequest = "";
                trajectoryRequest = String.Format("trajectory {0}\n", id);
                _socket.Send(trajectoryRequest);

                string trajectoryStr = ReadResponse();
                Console.WriteLine("done.");

                if (trajectoryStr != "> " && !trajectoryStr.Contains("\"atoms\": []"))
                {
                    TrajectoryParser trajectoryParser = new TrajectoryParser(trajectoryStr);
                    trajectories.Add(trajectoryParser.Parse());
                }
            }

            Console.WriteLine("Filtered out FahCore 17 slots, left with {0} trajectories.", trajectories.Count);

            return trajectories;
        }
        public string ReadResponse()
        {
            string pyon="";

            while (true)
            {
                string buffer = "";
                _socket.Recieve(out buffer);
                pyon += buffer;

                if (buffer.Contains("\n> ") || pyon == "> "            ||
                   (buffer.Contains("---")  && !buffer.Contains("---\nPyON")))
                    break; //reached end of message
            }

            return pyon;
        }

        private void ConnectToFAHClient()
        {
            Console.Write("Connecting to local FAHClient... ");
            string response = ReadResponse();

            if (!response.Contains("Welcome"))
                throw new Exception("Invalid response from FAHClient");

            Console.WriteLine("done. Got good response back.");
        }
        private void Authenticate()
        {
            if (!Options.GetInstance().UsesPassword)
                return;

            Console.Write("Authenticating to FAHClient... ");
            _socket.Send(String.Format("auth {0}\n", Options.GetInstance().Password));
            string response = ReadResponse();

            if (!response.Contains("OK"))
                throw new Exception(response);

            Console.WriteLine("Success!");
        }

        ClientSocket _socket;
    }
}
