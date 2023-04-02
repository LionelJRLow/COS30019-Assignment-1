using System;

namespace TreeBasedSearchAssignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Starter agent = new Starter("test.txt");
            Console.WriteLine("1 - BFS");
            Console.WriteLine("2 - DFS");
            Console.WriteLine("3 - GBFS");
            Console.WriteLine("4 - AStar");
            Console.WriteLine("5 - Uniform Cost");
            Console.WriteLine("6 - exit");

            bool exit;
            do
            {
                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        Console.Clear();
                        agent.BfsSearch();
                        exit = false;
                        break;

                    case "2":
                        Console.Clear();
                        agent.DfsSearch();
                        exit = false;
                        break;

                    case "3":
                        Console.Clear();
                        agent.GbfsSearch();
                        exit = false;
                        break;

                    case "4":
                        Console.Clear();
                        agent.AStarSearch();
                        exit = false;
                        break;

                    case "5":
                        Console.Clear();
                        agent.UniformSearch();
                        exit = false;
                        break;

                    case "6":
                        exit = false;
                        break;

                    default:
                        Console.Write("\rPlease enter valid response\n");
                        exit = true;
                        break;
                }

            } while (exit);
            
            //Console.Clear();
            //agent.BfsSearch();
            //Console.Clear();
            //agent.DfsSearch();
            //Console.Clear();
            //agent.GbfsSearch();
            //Console.Clear();
            //agent.AStarSearch();
            //Console.Clear();
            //agent.UniformSearch();
        }
    }
}
