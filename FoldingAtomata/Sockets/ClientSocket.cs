using System;
using System.Collections.Generic;

namespace FoldingAtomata.Sockets
{
    public class ClientSocket : FAHSocket
    {
        public ClientSocket(String host, int port)
        {
            if (!Create())
                throw new SocketException("Could not create client socket.");

            if (!Connect(host, port))
                throw new SocketException("Could not bind to port.");
        }
        public new void Send(string str)
        {
            if (base.Send(str))
                throw new SocketException("Could not write to socket.");
        }
        public void Recieve(out string str)
        {
            if (!(Recv(out str) == 0))
                throw new SocketException("Could not read from socket.");
        }
    }
}
