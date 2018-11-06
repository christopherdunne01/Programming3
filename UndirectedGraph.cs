using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM377CATMOUSEGAME
{
    class UndirectedGraph : GraphADT
    {
        private ArrayList nodes = new ArrayList();
        private int[,] adjMatrix;//Edges will be represented as adjacency Matrix
        //nodeID representing the ID for each added node should be increased by one every time when a node is added to the graph;
        private int nodeID = 0;
        public  const double INFINITY = Double.PositiveInfinity;


        //initialise adjacency matrix,  -1 stands for no edge connecting two nodes
        public UndirectedGraph(int size)
        {

            adjMatrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    adjMatrix[i, j] = -1;

        }

        //Add a node to graph
        public void AddNode(Node n)
        {
            nodes.Add(n);
            n.NodeID = nodeID;
            nodeID++;

        }

        //Gets the number of elements actually contained in the ArrayList.
        public int GetSize()
        {
            return nodes.Count;
        }

        //This method will be called to make connect two nodes
        public void SetEdge(Node start, Node end, int weight)
        {

            int startIndex = nodes.IndexOf(start);
            int endIndex = nodes.IndexOf(end);
            adjMatrix[startIndex, endIndex] = weight;
            adjMatrix[endIndex, startIndex] = weight;
            start.addNeighbor(end);
            end.addNeighbor(start);

        }

        //When removing the edge, the method is called
        public void DeleteEdge(Node start, Node end)
        {

            int startIndex = nodes.IndexOf(start);
            int endIndex = nodes.IndexOf(end);
            adjMatrix[startIndex, endIndex] = -1;
            adjMatrix[endIndex, startIndex] = -1;
        }

        //check whether there is an edge between two nodes
        public bool IsEdgeExists(Node start, Node end)
        {
            int startIndex = nodes.IndexOf(start);
            int endIndex = nodes.IndexOf(end);

            if (adjMatrix[startIndex, endIndex] != -1)
                return true;
            else
                return false;
        }

        private Node GetUnvisitedChildNode(Node n)
        {
            int indexParent = nodes.IndexOf(n);
            IEnumerator ie = n.GetNeighbors();
            while (ie.MoveNext())
            {
                Node neigh = ie.Current as Node;
                if (!neigh.IsVisited) return neigh;
            }
            return null;
        }

        //Utility methods for clearing visited property of node
        private void ClearNodes()
        {
            int i = 0;
            while (i < GetSize())
            {
                Node n = (Node)nodes[i];
                n.IsVisited = false;
                i++;
            }
        }

       

        //Utility methods for printing the node's label
        private void PrintNode(Node n)
        {
            Console.WriteLine(n.NodeLabel + " ");
        }

        //display the graph
        public void DumpGraph()
        {
            Dump(adjMatrix);
        }
        public void Dump(int[,] a)
        {
            int size = GetSize();
            Console.Write("\t");
            for (int s = 0; s < size; s = s + 1) Console.Write((Node)nodes[s]);
            Console.WriteLine();
            for (int s = 0; s < size; s = s + 1)
            {
                Console.Write((Node)nodes[s]);
                for (int e = 0; e < size; e = e + 1)
                {
                    if (a[s, e] == -1) Console.Write("F" + "\t");
                    else Console.Write("T" + "\t");
                }
                Console.WriteLine();
            }
        }

        //------------------------------------------------------------------------------------------- 
        // Return the weight of an edge given its two endpoints, or INFINITY if the edge doesn't exist.
        //   - Assume neither of v1 and v2 is null.
        //-------------------------------------------------------------------------------------------
        public double GetEdgeWeight(Node start, Node end)
        {

            int startIndex = nodes.IndexOf(start);
            int endIndex = nodes.IndexOf(end);

            if (adjMatrix[startIndex, endIndex] == -1) return INFINITY;

            return adjMatrix[startIndex, endIndex];
        }

        //------------------------------------------------------------------
        //Dijktras algorithm:Array contain short path weights, shortest path
        //predecessors and a flag to see if this node has been visited.
        //------------------------------------------------------------------

        public double GetShortestDistance(Node src, Node dst)
        {

            if (src == null || dst == null) return INFINITY;

            double[] pathWeights = new double[GetSize()];//short path weights associated with each node
            Node[] predecessors = new Node[GetSize()];//an array holds the shortest path predecessors

            //initialisation
            for (int i = 0; i < GetSize(); i++)
            {
                pathWeights[i] = INFINITY;//set to inifinite for every node at the beginning
                predecessors[i] = null;
            }

            ClearNodes();//clear the flag

            pathWeights[src.NodeID] = 0; //set to zero for the initial source node

            IEnumerator ie;
            Node temp = src;
            Node neigh;

            LinkedList<Node> nodeQueue = new LinkedList<Node>();//use a doubly linked list to hold a list of neighbouring nodes of the current node
            Enqueue(nodeQueue, pathWeights, src);
            while (!temp.Equals(dst) && nodeQueue.Count != 0)
            {
                temp = Dequeue(nodeQueue);
                
                if (!temp.IsVisited)
                {
                    temp.IsVisited = true;
                    nodeQueue.Clear();
                    ie = temp.GetNeighbors();
                    Console.WriteLine("Current node=" + temp.NodeLabel + "size of queue=" + nodeQueue.Count);
                    while (ie.MoveNext()) {
                        neigh = ie.Current as Node; ;
                        if (pathWeights[neigh.NodeID] > pathWeights[temp.NodeID] + GetEdgeWeight(temp, neigh))//temp is the predecessor node
                        {
                            pathWeights[neigh.NodeID] = pathWeights[temp.NodeID] + GetEdgeWeight(temp, neigh);
                            predecessors[neigh.NodeID] = temp;
                        }

                        Enqueue(nodeQueue, pathWeights, neigh);

                    }
                }
            }

            //construct the shortest path
            ArrayList path = new ArrayList();  // arraylist of nodes
            Node step = dst;
            while (step != null)
            {

                path.Add(step);//the first/last node in the path arraylist is the dst/src node
                step = predecessors[step.NodeID];
            }

            Node firstNode = path[path.Count - 1] as Node;
            if (!firstNode.Equals(src)) Console.WriteLine("There is something wrong with finding the shortest path");

            Console.Write("The shortest path:");//starting with the source node.
            for (int i = path.Count - 1; i >= 0; i--)
                Console.Write(path[i]);
            Console.WriteLine();

            return pathWeights[dst.NodeID];
        }

        public ArrayList GetShortestPath(Node src, Node dst)
        {
               //Console.WriteLine("src=(" + src.Row +"," + src.Column + "),dst=(" + dst.Row + "," + dst.Column + ")");
               //Console.WriteLine(GetShortestDistance(src, dst).ToString());
            if (src == null || dst == null) return null;

                //Console.WriteLine("finding shortest path...");

            double[] pathWeights = new double[GetSize()];//short path weights associated with each node
            Node[] predecessors = new Node[GetSize()];//an array holds the shortest path predecessors

            //initialisation
            for (int i = 0; i < GetSize(); i++)
            {
                pathWeights[i] = INFINITY;//set to inifinite for every node at the beginning
                predecessors[i] = null;
            }

            ClearNodes();//clear the flag

            pathWeights[src.NodeID] = 0; //set to zero for the initial source node

            IEnumerator ie;
            Node temp = src;
            Node neigh;

            LinkedList<Node> nodeQueue = new LinkedList<Node>();//use a doubly linked list to hold a list of neighbouring nodes of the current node
            Enqueue(nodeQueue, pathWeights, src);
            Console.WriteLine("size of the node queue=" + nodeQueue.Count);
            while (!temp.Equals(dst) && nodeQueue.Count != 0)
            {
                //Console.WriteLine("src is not equal to dst");
                temp = Dequeue(nodeQueue);

                if (!temp.IsVisited)
                {
                    temp.IsVisited = true;
                    //nodeQueue.Clear();
                    ie = temp.GetNeighbors();
                    //Console.WriteLine("Current node=(" + temp.Row + "," + temp.Column + "), size of queue=" + nodeQueue.Count);

                    while (ie.MoveNext()) {

                        neigh = ie.Current as Node;
                        if (pathWeights[neigh.NodeID] > pathWeights[temp.NodeID] + GetEdgeWeight(temp, neigh))
                        {
                            pathWeights[neigh.NodeID] = pathWeights[temp.NodeID] + GetEdgeWeight(temp, neigh);
                            predecessors[neigh.NodeID] = temp;
                        }

                        Enqueue(nodeQueue, pathWeights, neigh);

                    }
                }
            }

            //construct the shortest path

            ArrayList path = new ArrayList();  // arraylist of nodes
            Node step = dst;
            while (step != null)
            {
                path.Add(step);//the first node in the path arraylist is the destination node
                step = predecessors[step.NodeID];
            }
             
            if (path.Count > 1)
            {
                Node firstNode = path[path.Count - 1] as Node;
                if (!firstNode.Equals(src)) Console.WriteLine("There is something wrong with finding the shortest path");

            }

            return path;
        }

        //Enqueue operation
        //The node added to the queue is in the increasing order of pathweight, i.e the first node has a least pathweight
        private void Enqueue(LinkedList<Node> queue, double[] pathWeights, Node nodeToBeAdded)
        {
            if (queue.Count == 0)
            {
                queue.AddFirst(nodeToBeAdded);
                return;
            }
            LinkedListNode<Node> currentNode = queue.First;
            while (currentNode != null && CompareNodePathWeight(pathWeights, currentNode.Value, nodeToBeAdded) < 0)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode == null)
                queue.AddLast(nodeToBeAdded);
            else
            {
                queue.AddBefore(currentNode, nodeToBeAdded);
            }
        }

        private int CompareNodePathWeight(double[] pathWeights, Node n1, Node n2)
        {
            if (pathWeights[n1.NodeID] > pathWeights[n2.NodeID])
                return 1;
            if (pathWeights[n1.NodeID] < pathWeights[n2.NodeID])
                return -1;
            return 0;
        }


        //Dequeu operation
        private Node Dequeue(LinkedList<Node> queue)
        {
            Node dNode = queue.First.Value;
            queue.RemoveFirst();
            return dNode;
        }
    }
}
