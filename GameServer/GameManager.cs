using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class GameManager
    {
        public static List<Player> players = new List<Player>();
        static List<Player> redPlayers = new List<Player>();
        static List<Player> bluePlayers = new List<Player>();
        public static bool hasStarted = false;
        public static bool allPlayersReady;
        public static void GetConnectedPlayers()
        {
            allPlayersReady = true;
            players.Clear();
            redPlayers.Clear();
            bluePlayers.Clear();
            foreach (Client client in Server.clients.Values)
            {
                
                if (client.player != null)
                {
                    foreach (Player player in players)
                    {
                        ServerSend.TeamSync(player.id,client.id,player.team);
                    }
                    
                    players.Add(client.player);
                    
                    if (client.player.team == 0)
                    {
                        allPlayersReady = false;

                    }
                }

                
                
            }

           
            

            if (allPlayersReady && players.Count > 1)
            {
                foreach (Player player in players)
                {
                    if (player.team == 1)
                    {
                        redPlayers.Add(player);
                    }
                    else
                    {
                        bluePlayers.Add(player);
                    }
                }
                StartGame();
            }
        }

        private static void StartGame()
        {
            int redPlayer = Random(redPlayers.Count);
            int bluePlayer = Random(bluePlayers.Count);
            redPlayers[redPlayer].isLeader = true;
            bluePlayers[bluePlayer].isLeader = true;
            foreach (Player player in players)
            {
                ServerSend.StartGame(player.id,player.isLeader);
            }
            ServerSend.SendLeaders(redPlayers[redPlayer].id);
            ServerSend.SendLeaders(bluePlayers[bluePlayer].id);
            hasStarted = true;
            Console.WriteLine("Game started!");
        }

        private static int Random(int max)
        {
            Random random = new Random();
            int ans = random.Next(0, max);
            return ans;
        }
    }
}
