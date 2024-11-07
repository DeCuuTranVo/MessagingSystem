using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystem
{
    public class BFS_Result
    {
        public int user_id { get; set; } = 0;
        public int user_beaver_id { get; set; } = 0;
        public int shortest_path_length { get; set; } = 0; 
        public List<int> shortest_path { get; set; } = new List<int>();
    }
}
