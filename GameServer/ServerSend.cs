using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);

                SendUDPDataToAll(_player.id,_packet);
            }
        }

        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }

        public static void PlayerDisconnected(int _playerId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
            {
                _packet.Write(_playerId);
                SendTCPDataToAll(_packet);
            }
        }

        
        public static void BatRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.batRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.batRotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }

        public static void PlayerHit(int _toClient, int _attackStrength)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerHit))
            {
                _packet.Write(_attackStrength);


                SendTCPData(_toClient, _packet);
            }
        }

        public static void ParticleSync(int _fromClient,Vector3 _particlePosition)
        {
            using (Packet _packet = new Packet((int)ServerPackets.particleSync))
            {
                _packet.Write(_particlePosition);
                SendTCPDataToAll(_fromClient, _packet);
            }
        }

        public static void TeamSelect(int _fromClient,int _team)
        {
            using (Packet _packet = new Packet((int)ServerPackets.teamSelect))
            {
                _packet.Write(_fromClient);
                _packet.Write(_team);
                SendTCPDataToAll(_fromClient, _packet);
            }
        }

        public static void TeamSync(int _fromClient, int _toClient,int _team)
        {
            using (Packet _packet = new Packet((int)ServerPackets.teamSync))
            {
                _packet.Write(_fromClient);
                _packet.Write(_team);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void StartGame(int _toClient,bool _isleader)
        {
            using (Packet _packet = new Packet((int)ServerPackets.startGame))
            {
<<<<<<< HEAD
<<<<<<< HEAD
                
=======
>>>>>>> parent of 3b6207f (Fixed bug?)
=======
>>>>>>> parent of 3b6207f (Fixed bug?)
                _packet.Write(_isleader);               
                SendTCPData(_toClient, _packet);
            }
        }

        public static void SendLeaders(int _leaderId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.leader))
            {
                _packet.Write(_leaderId);
                SendTCPDataToAll(_leaderId, _packet);
            }
        }

        public static void EndGame(int _team)
        {
            using (Packet _packet = new Packet((int)ServerPackets.endGame))
            {
                _packet.Write(_team);
                SendTCPDataToAll( _packet);
            }
        }
        #endregion
    }
}
