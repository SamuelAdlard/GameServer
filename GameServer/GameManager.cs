using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class GameManager
    {
        
        static List<Player> redPlayers = new List<Player>();
        static List<Player> bluePlayers = new List<Player>();
        public static bool hasStarted = false;
        public static bool allPlayersReady = false;
        
        public static void CheckPlayers()
        {
            List<Player> players = new List<Player>();
            foreach (Client client in Server.clients.Values)
            {
                if (client.player != null)
                {
                    players.Add(client.player);
                }
                
            }
            allPlayersReady = true;
            foreach (Player player in players)
            {
                
                if (player != null)
                {
                    
                    
                    if (player.team == 0)
                    {
                        allPlayersReady = false;
                    }
                }
                
            }
            

            if (allPlayersReady && players.Count > 1)
            {
                foreach (Player player in players)
                {
                    if (player != null)
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
                    
                }
                StartGame(players);
            }
        }

        private static void StartGame(List<Player> players)
        {
            hasStarted = true;
            int redPlayer = Random(redPlayers.Count);
            int bluePlayer = Random(bluePlayers.Count);
            redPlayers[redPlayer].isLeader = true;
            bluePlayers[bluePlayer].isLeader = true;
            ServerSend.StartGame(redPlayers[redPlayer].id, true);
            ServerSend.StartGame(bluePlayers[bluePlayer].id, true);
            ServerSend.SendLeaders(redPlayers[redPlayer].id);
            ServerSend.SendLeaders(bluePlayers[bluePlayer].id);
            
            Console.WriteLine("Game started!");
        }

        public static void EndGame()
        {
            bluePlayers.Clear();
            redPlayers.Clear();
        }

        private static int Random(int max)
        {
            Random random = new Random();
            int ans = random.Next(0, max);
            return ans;
        }
    }
}
