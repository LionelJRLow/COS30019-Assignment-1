using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Linq;


namespace TreeBasedSearchAssignment1
{
    public class Agent
    {
        private Node root;
        private int goalx, goaly,length,width;
        private int steps = 0;
        private List<List<int>> wall;
        private List<Node> wallnodes = new List<Node>();
        private Stopwatch functiontime = new Stopwatch();
        private List<Node> path = new List<Node>();

        
        public Agent(int rootx,int rooty,int givengoalx,int givengoaly,int givlength,int givwidth,List<List<int>> walls)
        {
            root = new Node(rootx, rooty);
            goalx = givengoalx;
            goaly = givengoaly;
            length = givlength;
            width = givwidth;
            wall = walls;
           
            foreach (List<int> wall in walls)
            {
                for(int i = 0; i < wall[3]; i++)
                {
                    for (int j = 0; j < wall[2]; j++)
                    {
                        wallnodes.Add(new Node(wall[0]+j, wall[1]+i));
                    }
                }
            }
            //Console.WriteLine("Walls are: ");
            //foreach (Node node in wallnodes)
            //{
            //    Console.WriteLine("[{0},{1}]", node.X, node.Y);
            //}

        }

        public string BfsSearch()
        {
            steps = 0;
            Console.WriteLine("Currently running Bfs.");
            //to check runtime for function
            functiontime.Start();
            
            //if agent starts at goal 
            if ((root.X == goalx) && (root.Y == goaly))
            {
                functiontime.Stop();
                Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds / 1000+" seconds");    
                return "agent at goal already";
            }
                
            //queue for FIFO,visited to keep track of what we visited
            Queue<Node> frontier = new Queue<Node>();
            List<Node> visited = new List<Node>();
            //current node we are transversing
            Node currentlyvisit;
            //add agent initial position to frontier
            frontier.Enqueue(root);

            while (frontier.Count != 0)
            {
                //make current node the one that is removed from frontier
                currentlyvisit = frontier.Dequeue();
                //add current node to list of visited nodes
                visited.Add(currentlyvisit);
                steps++;
                
                //check if currently node is goal
                if (currentlyvisit.X == goalx && currentlyvisit.Y == goaly)
                {
                    //list for path to answer
                    List<string> travelled = new List<string>();
                    string answer = String.Format("[{0},{1}]", root.X, root.Y);
                    //get path from goal to agent
                    while (currentlyvisit.Parent != null)
                    {
                        string direction = " ";
                        //check if went right
                        if (currentlyvisit.X == currentlyvisit.Parent.X + 1)
                            direction = "=>right=>";
                        //check left
                        if (currentlyvisit.X == currentlyvisit.Parent.X - 1)
                            direction = "=>left=>";
                        //check down
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y + 1)
                            direction = "=>down=>";
                        //check up
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y - 1)
                            direction = "=>up=>";


                        string coord = String.Format("{0}[{1},{2}]",direction,currentlyvisit.X,currentlyvisit.Y);
                        travelled.Add(coord);
                        currentlyvisit = currentlyvisit.Parent;
                    }
                    //reverse the list so we can get agent to goal instead of goal to agent
                    travelled.Reverse();

                    for (int i = 0; i < travelled.Count; i++)
                    {
                        answer = answer +travelled[i];
                    }
                    functiontime.Stop();
                    Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds + " milliseconds");
                    string visitedstring = "";
                    //show all visited nodes
                    foreach (Node n in visited)
                    {
                        

                        visitedstring = visitedstring + String.Format("[{0},{1}];",n.X, n.Y);
                        //for(int i=0;i< width; i++)
                        //{
                        //    for(int j=0; j < length; j++)
                        //    {

                        //    }
                        //}
                    }
                    Console.WriteLine("Visited: " + visitedstring + "\n");

                    return String.Format("BFS Completed;\nAgent:[{0},{1}]\nGoal:[{2},{3}]\nPath: "+answer+"\nSteps: "+steps,root.X,root.Y,goalx,goaly);
                }

                //create adjacent nodes and add to root edges
                //check top adjacent within range of grid
                if (currentlyvisit.Y - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y - 1, currentlyvisit));
                //check left
                if (currentlyvisit.X - 1 >= 0 )
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X - 1, currentlyvisit.Y, currentlyvisit));
                //check bottom
                if (currentlyvisit.Y + 1 < width)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y + 1, currentlyvisit));
                //check right
                if (currentlyvisit.X + 1 < length)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X + 1, currentlyvisit.Y, currentlyvisit));

                //add each adjacent node to frontier
                foreach (Node edgecell in currentlyvisit.Edges)
                    //check if node does not exist in frontier,visited,walls and is not the agent initial position before adding
                    if (!frontier.Any(node=>node.X==edgecell.X&&node.Y==edgecell.Y) && !((edgecell.X==root.X)&&(edgecell.Y==root.Y))&&!visited.Any(node => node.X == edgecell.X && node.Y == edgecell.Y)&&!wallnodes.Any(node => node.X == edgecell.X && node.Y == edgecell.Y))
                        frontier.Enqueue(edgecell);

            }
            //if cant get answer
            return "failed to get solution";
        }

        public string DfsSearch()
        {
            steps = 0;
            Console.WriteLine("Currently running Dfs.");
            functiontime.Start();
            //if start at goal
            if ((root.X == goalx) && (root.Y == goaly))
            {
                functiontime.Stop();
                Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds / 1000 + " seconds");
                return "agent at goal already";
            }

            //stack for LIFO
            Stack<Node> frontier = new Stack<Node>();
            List<Node> visited = new List<Node>();

            Node currentlyvisit;

            frontier.Push(root);

            while (frontier.Count != 0)
            {

                currentlyvisit = frontier.Pop();
                visited.Add(currentlyvisit);
                steps++;


                //check if currently visit is goal
                if (currentlyvisit.X == goalx && currentlyvisit.Y == goaly)
                {
                    List<string> travelled = new List<string>();
                    string answer = String.Format("[{0},{1}]", root.X, root.Y);
                    //get path from goal to agent
                    while (currentlyvisit.Parent != null)
                    {
                        string direction = " ";
                        //check if went right
                        if (currentlyvisit.X == currentlyvisit.Parent.X + 1)
                            direction = "=>right=>";
                        //check left
                        if (currentlyvisit.X == currentlyvisit.Parent.X - 1)
                            direction = "=>left=>";
                        //check down
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y + 1)
                            direction = "=>down=>";
                        //check up
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y - 1)
                            direction = "=>up=>";


                        string coord = String.Format("{0}[{1},{2}]", direction, currentlyvisit.X, currentlyvisit.Y);
                        travelled.Add(coord);
                        currentlyvisit = currentlyvisit.Parent;
                    }
                    //reverse the list so we can get agent to goal instead of goal to agent
                    travelled.Reverse();

                    for (int i = 0; i < travelled.Count; i++)
                    {
                        answer = answer + travelled[i];
                    }
                    functiontime.Stop();
                    Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds + " milliseconds");
                    string visitedstring = "";
                    foreach (Node n in visited)
                    {
                        visitedstring = visitedstring + String.Format(" [{0},{1}];", n.X, n.Y);
                    }
                    Console.WriteLine("Visited: " + visitedstring + "\n");
                    return String.Format("DFS Completed;\nAgent:[{0},{1}]\nGoal:[{2},{3}]\nPath: " + answer+"\nSteps: "+steps, root.X, root.Y, goalx, goaly);
                }

                //create adjacent nodes and add to root edges
                //check top adjacent within range of grid
                if (currentlyvisit.Y - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y - 1, currentlyvisit));
                //check left
                if (currentlyvisit.X - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X - 1, currentlyvisit.Y, currentlyvisit));
                //check bottom
                if (currentlyvisit.Y + 1 < width)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y + 1, currentlyvisit));
                //check right
                if (currentlyvisit.X + 1 < length)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X + 1, currentlyvisit.Y, currentlyvisit));

                //add each adjacent node to frontier
                foreach (Node edgecell in currentlyvisit.Edges)
                    if (!frontier.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !((edgecell.X == root.X) && (edgecell.Y == root.Y)) && !visited.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !wallnodes.Any(node => node.X == edgecell.X && node.Y == edgecell.Y))
                        frontier.Push(edgecell);

            }
            return "failed to get solution";
        }

        public string GbfsSearch()
        {
            steps = 0;
            Console.WriteLine("Currently running Gbfs.");
            functiontime.Start();
            //if start at goal
            if ((root.X == goalx) && (root.Y == goaly))
            {
                functiontime.Stop();
                Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds / 1000 + " seconds");
                return "agent at goal already";
            }

            List<Node> frontier = new List<Node>();
            List<Node> visited = new List<Node>();

            Node currentlyvisit;
            frontier.Add(root);
            //for (int i = 0; i < width; i++)
            //    for (int j = 0; j < length; j++)
            //    {
            //        if (!frontier.Any(node => node.X == j && node.Y == i))
            //            frontier.Add(new Node(j,i,goalx,goaly));
            //    }
            
                
            while (frontier.Count != 0)
            {
                //order lowest distance from goal
                frontier = frontier.OrderBy(node => Math.Abs(goalx - node.X) + Math.Abs(goaly - node.Y)).ToList();
                //string frontierstring = "";
                //foreach(Node n in frontier)
                //{
                //    frontierstring = frontierstring + String.Format("[{0},{1},{2}]; ",n.X,n.Y,n.Distancegoal);
                //}
              //  Console.WriteLine("Frontier order: {0},{1}",goalx,goaly);
                //Console.WriteLine(frontierstring);

                currentlyvisit = frontier.First();
                frontier.Remove(frontier.First());
                visited.Add(currentlyvisit);
                steps++;

                //check if currently visit is goal
                if (currentlyvisit.X == goalx && currentlyvisit.Y == goaly)
                {
                    List<string> travelled = new List<string>();
                    string answer = String.Format("[{0},{1}]", root.X, root.Y);
                    //get path from goal to agent
                    while (currentlyvisit.Parent != null)
                    {
                        string direction = " ";
                        //check if went right
                        if (currentlyvisit.X == currentlyvisit.Parent.X + 1)
                            direction = "=>right=>";
                        //check left
                        if (currentlyvisit.X == currentlyvisit.Parent.X - 1)
                            direction = "=>left=>";
                        //check down
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y + 1)
                            direction = "=>down=>";
                        //check up
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y - 1)
                            direction = "=>up=>";


                        string coord = String.Format("{0}[{1},{2}]", direction, currentlyvisit.X, currentlyvisit.Y);
                        travelled.Add(coord);
                        currentlyvisit = currentlyvisit.Parent;
                    }
                    //reverse the list so we can get agent to goal instead of goal to agent
                    travelled.Reverse();

                    for (int i = 0; i < travelled.Count; i++)
                    {
                        answer = answer + travelled[i];
                    }
                    functiontime.Stop();
                    Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds + " milliseconds");
                    string visitedstring = "";
                    foreach (Node n in visited)
                    {
                        visitedstring = visitedstring + String.Format(" [{0},{1}];", n.X, n.Y);
                    }
                    Console.WriteLine("Visited: " + visitedstring + "\n");
                    return String.Format("GBFS Completed;\nAgent:[{0},{1}]\nGoal:[{2},{3}]\nPath: " + answer + "\nSteps: " + steps, root.X, root.Y, goalx, goaly);
                }
                //create adjacent nodes and add to root edges
                //check top adjacent within range of grid
                if (currentlyvisit.Y - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y - 1, currentlyvisit));
                //check left
                if (currentlyvisit.X - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X - 1, currentlyvisit.Y, currentlyvisit));
                //check bottom
                if (currentlyvisit.Y + 1 < width)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y + 1, currentlyvisit));
                //check right
                if (currentlyvisit.X + 1 < length)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X + 1, currentlyvisit.Y, currentlyvisit));

                //add each adjacent node to frontier
                foreach (Node edgecell in currentlyvisit.Edges)
                {
                    if (!frontier.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !((edgecell.X == root.X) && (edgecell.Y == root.Y)) && !visited.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !wallnodes.Any(node => node.X == edgecell.X && node.Y == edgecell.Y))
                        frontier.Add(edgecell);
                }
                    

            }


            return "failed to get solution";
        }

        public string AStarSearch()
        {
            steps = 0;
            Console.WriteLine("Currently running AStar.");
            functiontime.Start();
            //if start at goal
            if ((root.X == goalx) && (root.Y == goaly))
            {
                functiontime.Stop();
                Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds / 1000 + " seconds");
                return "agent at goal already";
            }

            List<Node> frontier = new List<Node>();
            List<Node> visited = new List<Node>();

            Node currentlyvisit;
            frontier.Add(root);
            while (frontier.Count != 0)
            {
                //order lowest distance from goal+distance from agent
                frontier = frontier.OrderBy(node => (Math.Abs(goalx - node.X) + Math.Abs(goaly - node.Y)) + (Math.Abs(root.X - node.X) + Math.Abs(root.Y - node.Y))).ToList();

                currentlyvisit = frontier.First();
                frontier.Remove(frontier.First());
                visited.Add(currentlyvisit);
                steps++;

                //check if currently visit is goal
                if (currentlyvisit.X == goalx && currentlyvisit.Y == goaly)
                {
                    List<string> travelled = new List<string>();
                    string answer = String.Format("[{0},{1}]", root.X, root.Y);
                    //get path from goal to agent
                    while (currentlyvisit.Parent != null)
                    {
                        string direction = " ";
                        //check if went right
                        if (currentlyvisit.X == currentlyvisit.Parent.X + 1)
                            direction = "=>right=>";
                        //check left
                        if (currentlyvisit.X == currentlyvisit.Parent.X - 1)
                            direction = "=>left=>";
                        //check down
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y + 1)
                            direction = "=>down=>";
                        //check up
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y - 1)
                            direction = "=>up=>";


                        string coord = String.Format("{0}[{1},{2}]", direction, currentlyvisit.X, currentlyvisit.Y);
                        travelled.Add(coord);
                        currentlyvisit = currentlyvisit.Parent;
                    }
                    //reverse the list so we can get agent to goal instead of goal to agent
                    travelled.Reverse();

                    for (int i = 0; i < travelled.Count; i++)
                    {
                        answer = answer + travelled[i];
                    }
                    functiontime.Stop();
                    Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds + " milliseconds");
                    string visitedstring = "";
                    foreach (Node n in visited)
                    {
                        visitedstring = visitedstring + String.Format(" [{0},{1}];", n.X, n.Y);
                    }
                    Console.WriteLine("Visited: " + visitedstring + "\n");
                    return String.Format("AStar Completed;\nAgent:[{0},{1}]\nGoal:[{2},{3}]\nPath: " + answer+"\nSteps: "+steps, root.X, root.Y, goalx, goaly);
                }
                //create adjacent nodes and add to root edges
                //check top adjacent within range of grid
                if (currentlyvisit.Y - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y - 1, currentlyvisit));
                //check left
                if (currentlyvisit.X - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X - 1, currentlyvisit.Y, currentlyvisit));
                //check bottom
                if (currentlyvisit.Y + 1 < width)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y + 1, currentlyvisit));
                //check right
                if (currentlyvisit.X + 1 < length)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X + 1, currentlyvisit.Y, currentlyvisit));

                //add each adjacent node to frontier
                foreach (Node edgecell in currentlyvisit.Edges)
                    if (!frontier.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !((edgecell.X == root.X) && (edgecell.Y == root.Y)) && !visited.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !wallnodes.Any(node => node.X == edgecell.X && node.Y == edgecell.Y))
                        frontier.Add(edgecell);

            }


            return "failed to get solution";
        }

        public string UniformSearch()
        {
            steps = 0;
            Console.WriteLine("Currently running UniformSearch.");
            functiontime.Start();
            //if start at goal
            if ((root.X == goalx) && (root.Y == goaly))
            {
                functiontime.Stop();
                Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds / 1000 + " seconds");
                return "agent at goal already";
            }

            List<Node> frontier = new List<Node>();
            List<Node> visited = new List<Node>();

            Node currentlyvisit;
            frontier.Add(root);
            while (frontier.Count != 0)
            {
                //order lowest distance from agent
                frontier = frontier.OrderBy(node => (Math.Abs(root.X - node.X) + Math.Abs(root.Y - node.Y))).ToList();

                currentlyvisit = frontier.First();
                frontier.Remove(frontier.First());
                visited.Add(currentlyvisit);
                steps++;

                //check if currently visit is goal
                if (currentlyvisit.X == goalx && currentlyvisit.Y == goaly)
                {
                    List<string> travelled = new List<string>();
                    string answer = String.Format("[{0},{1}]", root.X, root.Y);
                    //get path from goal to agent
                    while (currentlyvisit.Parent != null)
                    {
                        string direction = " ";
                        //check if went right
                        if (currentlyvisit.X == currentlyvisit.Parent.X + 1)
                            direction = "=>right=>";
                        //check left
                        if (currentlyvisit.X == currentlyvisit.Parent.X - 1)
                            direction = "=>left=>";
                        //check down
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y + 1)
                            direction = "=>down=>";
                        //check up
                        if (currentlyvisit.Y == currentlyvisit.Parent.Y - 1)
                            direction = "=>up=>";


                        string coord = String.Format("{0}[{1},{2}]", direction, currentlyvisit.X, currentlyvisit.Y);
                        travelled.Add(coord);
                        currentlyvisit = currentlyvisit.Parent;
                    }
                    //reverse the list so we can get agent to goal instead of goal to agent
                    travelled.Reverse();

                    for (int i = 0; i < travelled.Count; i++)
                    {
                        answer = answer + travelled[i];
                    }
                    functiontime.Stop();
                    Console.WriteLine("Function execution time: " + functiontime.ElapsedMilliseconds + " milliseconds");
                    string visitedstring = "";
                    foreach (Node n in visited)
                    {
                        visitedstring = visitedstring + String.Format(" [{0},{1}];", n.X, n.Y);
                    }
                    Console.WriteLine("Visited: " + visitedstring + "\n");
                    return String.Format("Uniform Cost Completed;\nAgent:[{0},{1}]\nGoal:[{2},{3}]\nPath: " + answer + "\nSteps: " + steps, root.X, root.Y, goalx, goaly);
                }
                //create adjacent nodes and add to root edges
                //check top adjacent within range of grid
                if (currentlyvisit.Y - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y - 1, currentlyvisit));
                //check left
                if (currentlyvisit.X - 1 >= 0)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X - 1, currentlyvisit.Y, currentlyvisit));
                //check bottom
                if (currentlyvisit.Y + 1 < width)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X, currentlyvisit.Y + 1, currentlyvisit));
                //check right
                if (currentlyvisit.X + 1 < length)
                    currentlyvisit.Edges.Add(new Node(currentlyvisit.X + 1, currentlyvisit.Y, currentlyvisit));

                //add each adjacent node to frontier
                foreach (Node edgecell in currentlyvisit.Edges)
                    if (!frontier.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !((edgecell.X == root.X) && (edgecell.Y == root.Y)) && !visited.Any(node => node.X == edgecell.X && node.Y == edgecell.Y) && !wallnodes.Any(node => node.X == edgecell.X && node.Y == edgecell.Y))
                        frontier.Add(edgecell);

            }


            return "failed to get solution";
        }
    }
}
