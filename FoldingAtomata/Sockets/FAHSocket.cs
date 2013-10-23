using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace FoldingAtomata.Sockets
{
    public class FAHSocket
    {
        public FAHSocket()
        {
            //_sock = -1;
            //memset(_address, 0, sizeof(_address));

            //TcpListener listener = new TcpListener(port);
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }
        ~FAHSocket()
        {
            if (IsValid())
                _socket.Close();
        }

        public bool Create()
        {
            /*_sock = new Socket(AF_INET, SOCK_STREAM, 0);

            if (!IsValid()) return false;

            int on = 1;
            return setsockopt(_sock, SOL_SOCKET, SO_REUSEADDR, on, sizeof(on)) != -1;
            */

            return true;
        }
        public bool Bind(int port)
        {
            /*
            if (!IsValid()) return false;

            _address.sin_family = AF_INET;
            _address.sin_addr.s_addr = INADDR_ANY;
            _address.sin_port = htons(port);

            int bind_return = bind(_sock, _address, sizeof(_address));

            return bind_return != -1;
            */

            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
            _socket.Bind(localEP);
            return _socket.IsBound;
        }
        public bool Listen()
        {
            /*
            if (!IsValid()) return false;

            int listen_return = listen(_sock, MAX_CONNECTIONS);
            return listen_return != -1;
            */

            _socket.Listen(100);
            return true;
        }
        public bool Accept()
        {
            /*
            FAHSocket socket
            int addr_length = sizeof(_address);
            new_socket._sock = accept(_sock, _address, addr_length);

            return new_socket._sock > 0;
            */
            Socket _new = _socket.Accept();
            return _new != null;
        }
        public bool Connect(string host, int port)
        {
            /*
            if (!IsValid())
                return false;

            address_.sin_family = AF_INET;
            address_.sin_port = htons(port);

            int status = inet_pton(AF_INET, host, _address.sin_addr);

            if (errno == EAFNOSUPPORT)
                return false;

            status = connect(_sock, _address, sizeof(_address));
            return status == 0;
             * */

            try
            {
                _socket.Connect(host, port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception at FAHSocket.Connect: {0}", e);
            }
            return _socket.Connected;
        }
        public bool Send(string msg)
        {
            //long status = send(_sock, msg, msg.Length, MSG_NOSIGNAL);
            //return status != -1L;
            SocketError error;
            byte[] data = msg.ToCharArray().ToByteArray();
            int o = _socket.Send(data, 0, data.Length, SocketFlags.None, out error);
            Console.WriteLine("Socket Send Error Report: {0}", error.ToString());
            return o == data.Length;
        }
        public long Recv(out string msg)
        {
            /*
            char buf[MAX_RECV + 1];
            memset(buf, 0, MAX_RECV + 1);

            msg = "";

            long status = recv(_sock, buf, MAX_RECV, 0);

            if (status == -1L)
            {
                Console.WriteLine("Recv error, status -1,{0}", errno);
                return 0;
            }
            else if (status == 0L)
            {
                return 0;
            }
            else
            {
                msg = buf;
                return status;
            }
             * */

            SocketError error;
            byte[] data = new byte[int.MaxValue];
            int i = _socket.Receive(data, 0, data.Length, SocketFlags.None, out error);
            Console.WriteLine("Socket Receive Error Report: {0}", error.ToString());
            msg = new string(data.ToCharArray());
            return i;
        }
        public void SetNonBlocking(bool blocking)
        {
            /*
            int opts;
            opts = fcntl(_sock, F_GETFL);

            if (opts < 0)
                return;

            if (blocking)
                opts = (opts | O_NONBLOCK);
            else
                opts = (opts & ~O_NONBLOCK);

            fcntl(_sock, F_SETFL, opts);
             * */

            _socket.Blocking = blocking;
        }
        public bool IsValid()
        {
            //return _sock != -1;
            return true;
        }

        Socket _socket;
    }
}
