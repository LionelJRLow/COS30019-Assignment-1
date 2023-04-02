using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace TreeBasedSearchAssignment1
{
    public class Starter
    {
        FileReader reader;
        Agent treebased;
        Agent treebased2;
        public Starter(string textfile)
        {
            FileReader reader = new FileReader(textfile);
            reader.Read();
            List<int> gridsize = reader.getGrid();
            List<int> agentloc = reader.getAgent();
            List<int> goalloc = reader.getGoal();
            List<List<int>> walls = reader.getWall();
            treebased = new Agent(agentloc[0],agentloc[1],goalloc[0],goalloc[1],gridsize[1],gridsize[0],walls);
            treebased2 = new Agent(agentloc[0], agentloc[1], goalloc[2], goalloc[3], gridsize[1], gridsize[0], walls);
            
        }
        public string Draw()
        {
            return "";
        }

        public void BfsSearch()
        {
            Console.WriteLine(treebased.BfsSearch());
            Console.WriteLine("Press enter to show BFS for second goal");
            Console.ReadLine();
            Console.WriteLine(treebased2.BfsSearch());
            Console.WriteLine("Press enter to exit BFS");
            Console.ReadLine();
      
        }

        public void DfsSearch()
        {
            Console.WriteLine(treebased.DfsSearch());
            Console.WriteLine("\rPress enter to show DFS for second goal");
            Console.ReadLine();
            Console.WriteLine(treebased2.DfsSearch());
            Console.WriteLine("Press enter to exit DFS");
            Console.ReadLine();
    
        }

        public void GbfsSearch()
        {
            Console.WriteLine(treebased.GbfsSearch());
            Console.WriteLine("\rPress enter to show GBFS for second goal");
            Console.ReadLine();
            Console.WriteLine(treebased2.GbfsSearch());
            Console.WriteLine("Press enter to exit GBFS");
            Console.ReadLine();
       

        }

        public void AStarSearch()
        {
            Console.WriteLine(treebased.AStarSearch());
            Console.WriteLine("\rPress enter to show GBFS for second goal");
            Console.ReadLine();
            Console.WriteLine(treebased2.AStarSearch());
            Console.WriteLine("Press enter to exit AStar");
            Console.ReadLine();
        }

        public void UniformSearch()
        {
            Console.WriteLine(treebased.UniformSearch());
            Console.WriteLine("\rPress enter to show Uniform Cost for second goal");
            Console.ReadLine();
            Console.WriteLine(treebased2.UniformSearch());
            Console.WriteLine("Press enter exit Uniform Cost");
            Console.ReadLine();
        }
    }
}
