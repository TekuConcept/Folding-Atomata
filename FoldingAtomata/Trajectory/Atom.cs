using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NTrajectory
{
    public class Atom
    {
        public Atom(string symbol, int atomicNumber, float charge, float radius, float mass)
        {
            Symbol = symbol;
            AtomicNumber = atomicNumber;
            Charge = charge;
            Radius = radius;
            Mass = mass;
        }

        public Vector3 Color
        {
            get
            {
                switch (Element)
                {
                    case 'H' : //hydrogen
                        return new Vector3(0.933f, 0.933f, 0.933f); //238, 238, 238

                    case 'C' : //carbon
                        return new Vector3(0.133f, 0.133f, 0.133f); //34, 34, 34

                    case 'N' : //nitrogen
                        return new Vector3(0.133f, 0.2f, 1.0f); //34, 51, 255

                    case 'O' : //oxygen
                        return new Vector3(1.0f, 0.133f, 0.0f); //255, 34, 0

                    case 'S' : //sulfur
                        return new Vector3(0.866f, 0.866f, 0.0f); //221, 221, 0

                    default : //all other elements
                        return new Vector3(0.866f, 0.466f, 1.0f); //221, 119, 255
                }
            }
        }
        public char Element
        {
            get
            {
                return Symbol[0];
            }
        }
        public string Symbol { get; private set; }
        public int AtomicNumber { get; private set; }
        public float Charge { get; private set; }
        public float Radius { get; private set; }
        public float Mass { get; private set; }
        public float ElectronShellCount
        {
            get
            {
                switch (Element)
                {
                    case 'H':
                        return 1;

                    case 'C':
                        return 2;

                    case 'N':
                        return 2;

                    case 'O':
                        return 2;

                    case 'S':
                        return 3;

                    default:
                        return 3;
                }
            }
        }
    }
}
