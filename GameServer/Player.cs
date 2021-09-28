using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;
        public bool isLeader = false;
        public int team = 0;
        public Vector3 position;
        public Quaternion rotation;
        public Quaternion batRotation;



        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            batRotation = Quaternion.Identity;

        }

        

        

        public void SetPosition(Vector3 _position, Quaternion _batRotation,Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
            batRotation = _batRotation;
            ServerSend.PlayerPosition(this);
            ServerSend.BatRotation(this);
            ServerSend.PlayerRotation(this);
        }
    }
}