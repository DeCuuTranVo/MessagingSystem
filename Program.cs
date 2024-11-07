using MessagingSystem;

Follow follow1 = new Follow { follow_id = 1, from_user_id = 1, to_user_id = 2 };
Follow follow2 = new Follow { follow_id = 2, from_user_id = 2, to_user_id = 3 };
Follow follow3 = new Follow { follow_id = 3, from_user_id = 1, to_user_id = 3 };
Follow follow4 = new Follow { follow_id = 4, from_user_id = 3, to_user_id = 4 };
Follow follow5 = new Follow { follow_id = 5, from_user_id = 4, to_user_id = 1 };
Follow follow6 = new Follow { follow_id = 6, from_user_id = 4, to_user_id = 5 };
Follow follow7 = new Follow { follow_id = 7, from_user_id = 2, to_user_id = 5 };

List<Follow> follows = new List<Follow>();
follows.Add(follow1);
follows.Add(follow2);
follows.Add(follow3);
follows.Add(follow4);
follows.Add(follow5);
follows.Add(follow6);
follows.Add(follow7);
Console.WriteLine("Input: " + "[" + String.Join(", ", follows.Select(x => x.from_user_id + " -> " + x.to_user_id)) + "]");

Console.WriteLine("-----------------------------------------------------------------");
BFS bfs_algo = new BFS(follows);
Console.WriteLine("Adjacent list:");
foreach (var item in bfs_algo.adjacentList)
{
    Console.WriteLine(item.Key + "-> [ " + String.Join(", ", item.Value) + "]");
}

Console.WriteLine("-----------------------------------------------------------------");
BFS_Result result = bfs_algo.breath_first_search(user_beaver_id: 1, user_id: 5);
Console.WriteLine("Beaver Operations:");
Console.WriteLine("From user: " + result.user_beaver_id);
Console.WriteLine("To user: " + result.user_id);
Console.WriteLine("Beaver number: " + result.shortest_path_length);
Console.WriteLine("Path: [ " + String.Join(" -> ", result.shortest_path) + "]");


