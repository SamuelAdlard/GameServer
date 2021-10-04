using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Numerics;
namespace GameServer
{
    
    
    class BoatManager
    {
        public static Dictionary<int, Boat> boats = new Dictionary<int, Boat>();
        static int boatNum = 0;
        public static void NewBoat(Vector3 spawnPosition, Quaternion rotation)
        {
            boats.Add(boatNum, new Boat(spawnPosition, rotation, false));
            ServerSend.NewBoat(spawnPosition,rotation,boatNum);
            boatNum++;
            
        }

        public static void UpdateTransform(Vector3 _position,Quaternion _rotation, int _id, bool drivingBoat, int _fromClient)
        {
            boats[_id].position = _position;
            boats[_id].rotation = _rotation;
            boats[_id].isDriven = drivingBoat;
            ServerSend.Boat(boats[_id].position, boats[_id].rotation, _id, boats[_id].isDriven, _fromClient);
            
        }

        public static void Dismount(int _id)
        {
            if (boats.ContainsKey(_id))
            {
                boats[_id].isDriven = false;
                ServerSend.Dismount(_id);
            }
            
        }
    }
}
