using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }


        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            
            Vector3 _position = _packet.ReadVector3();
            Quaternion _batRotation = _packet.ReadQuaternion();
            Quaternion _rotation = _packet.ReadQuaternion();
            if (Server.clients[_fromClient] != null)
            {
                if (Server.clients[_fromClient].player != null)
                {
                    Server.clients[_fromClient].player.SetPosition(_position, _batRotation, _rotation);
                }
                
            }
            
        }

        public static void PlayerHit(int _fromClient, Packet _packet)
        {
            
            int _toClient = _packet.ReadInt();
            int _attackStrength = _packet.ReadInt();
            Vector3 _particlePosition = _packet.ReadVector3();
            if (GameManager.hasStarted)
            {
                ServerSend.PlayerHit(_toClient, _attackStrength);
            }
            
            ServerSend.ParticleSync(_fromClient, _particlePosition);

        }

        public static void TeamSelect(int _fromClient, Packet _packet)
        {
            int team = _packet.ReadInt();
            Server.clients[_fromClient].player.team = team;
            ServerSend.TeamSelect(_fromClient,team);
        }

        public static void EndGame(int _fromClient, Packet _packet)
        {
            foreach (Player player in GameManager.players)
            {
                player.team = 0;
            }
            GameManager.hasStarted = false;
            
            int _team = _packet.ReadInt();
            ServerSend.EndGame(_team);
            Console.WriteLine("Game ended!");

        }
    }
}