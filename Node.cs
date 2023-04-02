using System;
using System.Collections.Generic;
using System.Text;

namespace TreeBasedSearchAssignment1
{
    public class Node
    {
        private int x = 0, y = 0;
        private bool wallstatus=false;
        private Node parent=null;
        private List<Node> edges= new List<Node>();

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public bool Wallstatus { get => wallstatus; set => wallstatus = value; }
        public Node Parent { get => parent; set => parent = value; }
        public List<Node> Edges { get => edges; set => edges = value; }

        public Node(int givx, int givy)
        {
            x = givx;
            y = givy;
        }

        public Node(int givx, int givy,Node parentnode)
        {
            x = givx;
            y = givy;
            parent = parentnode;
        }


    }
}
