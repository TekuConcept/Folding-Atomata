using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NTrajectory
{
    public class Snapshot
    {
        public void AddPosition(Vector3 position)
        {
            _positions.Add(position);
        }
        public Vector3 GetPosition(int atomIndex)
        {
            if (atomIndex >= _positions.Count)
            {
                String stream = "";
                stream = String.Format("Index {0} out of [0,{1}] bounds!", atomIndex, _positions.Count);
                throw new Exception(stream);
            }

            return _positions[atomIndex];
        }

        List<Vector3> _positions = new List<Vector3>();
    }
}
