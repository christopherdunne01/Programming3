using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM377CATMOUSEGAME
{
    class Node
    {
        private string nodeLabel;
        private bool visited = false;
        private ArrayList neighbors;  // neighbors of the node
        private int nodeID = 0;

        public int arrayNeighbors()
        {
            return neighbors.Count;
        }

        private int row;
        private int column;

        //constructor
        public Node(String label)
        {
            this.nodeLabel = label;
            neighbors = new ArrayList();
        }

        //Another constructor
        public Node(int row, int column)
        {
            this.row = row;
            this.column = column;

            neighbors = new ArrayList();
        }

        //property for visited 
        public bool IsVisited
        {
            get
            {
                return visited;
            }

            set
            {
                visited = value;

            }
        }

        //property for node label 
        public string NodeLabel
        {
            get
            {
                return nodeLabel;
            }

            set
            {
                nodeLabel = value;

            }
        }

        //property for node id 
        public int NodeID
        {
            get
            {
                return nodeID;
            }

            set
            {
                nodeID = value;

            }
        }

        //property for row
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }


        //property for column
        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
        }

        // Add a neighbor to the node
        public void addNeighbor(Node v)
        {
            neighbors.Add(v);
        }

        // Return an iterator of neighboring nodes
        public IEnumerator GetNeighbors()
        {
            return neighbors.GetEnumerator();
        }

        //toString method
        public override string ToString()
        {
            return (nodeLabel + "\t");
        }
    }
}
