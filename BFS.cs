using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystem
{
    public class BFS
    {
        // number of node in graph
        private int n = 0;
        // adjacent list representing unweighted graph
        public Dictionary<int, List<int>> adjacentList;

        public BFS(List<Follow> follows) { 
            construct_adjacent_list(follows);
        }

        private void construct_adjacent_list(List<Follow> follows)
        {
            adjacentList = new Dictionary<int, List<int>>();

            foreach (Follow item in follows)
            {
                if (!adjacentList.ContainsKey(item.from_user_id))
                {
                    List<int> to_user_id_list = new List<int>();
                    to_user_id_list.Add(item.to_user_id);
                    adjacentList.Add(item.from_user_id, to_user_id_list);
                }
                else
                {
                    adjacentList[item.from_user_id].Add(item.to_user_id);
                }

                if (!adjacentList.ContainsKey(item.to_user_id))
                {
                    adjacentList.Add(item.to_user_id, new List<int>());
                }
            }

            n = adjacentList.Count();            
        }

        public BFS_Result breath_first_search(int user_id, int user_beaver_id)
        {
            var prev = solve(user_beaver_id);
            List<int> shortest_path = reconstruct_path(user_beaver_id, user_id, prev);

            return new BFS_Result
            {
                user_id = user_id,
                user_beaver_id = user_beaver_id,
                shortest_path_length = shortest_path.Count - 1,
                shortest_path = shortest_path
            };
        }

        private Dictionary<int, int?> solve(int user_beaver_id)
        {
            Queue<int> vertices_of_interest = new Queue<int>();
            vertices_of_interest.Enqueue(user_beaver_id);

            // Tracking visited nodes
            Dictionary<int, bool> visited = new Dictionary<int, bool>();
            foreach (var item in adjacentList)
            {
                visited.Add(item.Key, false);
            }
            visited[user_beaver_id] = true;

            // Tracking previous node of current node
            Dictionary<int, int?> prev = new Dictionary<int, int?>();
            foreach (var item in adjacentList)
            {
                prev.Add(item.Key, null);
            }

            while (vertices_of_interest.Count()!=0)
            {
                int current_node = vertices_of_interest.Dequeue();
                List<int> current_neighbors = adjacentList[current_node];
                
                foreach (int next_node in current_neighbors)
                {
                    if (!visited[next_node])
                    {
                        vertices_of_interest.Enqueue(next_node);
                        visited[next_node] = true;
                        prev[next_node] = current_node;
                    }
                }
            }

            //Console.WriteLine("Visited " + ": [ " + String.Join(", ", visited) + "]");
            //Console.WriteLine("Prev " + ": [ " + String.Join(", ", prev) + "]");

            return prev;
        }

        private List<int> reconstruct_path(int user_beaver_id, int user_id, Dictionary<int, int?> prev)
        {
            // Reconstruct path going backwards from user_id
            List<int> path = new List<int>();
            for (int? cur_node = user_id; cur_node != null; cur_node = prev[(int)cur_node]) {
                path.Add((int)cur_node);
            }

            path.Reverse();

            // If user_beaver_id and user_id are connected, return the path
            if (path[0] == user_beaver_id)
            {
                return path;
            }
            
            return new List<int>();
        }
    }
}
