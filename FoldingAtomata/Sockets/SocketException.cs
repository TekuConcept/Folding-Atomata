using System;
using System.Collections.Generic;

namespace FoldingAtomata.Sockets
{
    public class SocketException : Exception
    {
        public SocketException(string description)
            : base(description)
        {

        }
    }
}
