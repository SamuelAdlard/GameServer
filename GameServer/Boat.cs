using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GameServer
{
    class Boat
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool isDriven;

        public Boat(Vector3 _position, Quaternion _rotation, bool _isDriven)
        {
            position = _position;
            rotation = _rotation;
            isDriven = _isDriven;
        }
    }
}
