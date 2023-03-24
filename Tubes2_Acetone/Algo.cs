using System;
namespace Tubes2_Acetone
{
	public class Algorithm
	{
        // Jumlah Node
        public static int CountNodes(Node[,] map) {
            int nodeCounted = 0;
            for (int i=0; i < map.GetLength(0); i++) {
                for (int j=0; j < map.GetLength(1); j++) {
                    if (map[i,j] != null) {
                        nodeCounted++;
                    }
                }
            }
            return nodeCounted;
        }

        public static int CountSteps(Node[] solution) {
            return solution.Length;
        }

        public static string[] GetRoute(Node[] solution) {
            string[] route = { };
            for (int i = 0; i < solution.Length-1; i++) {
                if (solution[i + 1] == solution[i].getRight())
                {
                    string[] add = { "R" };
                    route = ConcatString(route, add);
                }
                else
                {
                    if (solution[i + 1] == solution[i].getDown())
                    {
                        string[] add = { "D" };
                        route = ConcatString(route, add);
                    }
                    else
                    {
                        if (solution[i + 1] == solution[i].getLeft())
                        {
                            string[] add = { "L" };
                            route = ConcatString(route, add);
                        } else {
                            if (solution[i + 1] == solution[i].getUp())
                            {
                                string[] add = { "U" };
                                route = ConcatString(route, add);
                            }
                        }
                    }
                }
            }
            return route;
        }
        
        // Fungsi dasar
        // Concat string
        public static string[] ConcatString(string[] first, string[] second) {
            var combinedArray = new string[first.Length + second.Length];
            first.CopyTo(combinedArray, 0);
            second.CopyTo(combinedArray, first.Length);
            return combinedArray;
        }

        // To add 2 Nodes
        public static Node[] ConcatNode(Node[] firstArray, Node[] secondArray)
        {
            var combinedArray = new Node[firstArray.Length + secondArray.Length];
            firstArray.CopyTo(combinedArray, 0);
            secondArray.CopyTo(combinedArray, firstArray.Length);
            return combinedArray;
        }

        // To check treasure consisting node
        public static Tuple<Node[], int> CheckTreasure(Node currentNode, int treasureLeft)
        {
            Node[] nodeChecked = { };
            int treasureFounded = 0;

            // Check right
            if (currentNode.getRight() != null)
            {
                Node? nodeToAdd = currentNode.getRight();
                currentNode.checkNode();
                if (nodeToAdd?.getTimesChecked() == 0)
                {
                    Node[] toAddArray = { nodeToAdd };
                    nodeChecked = ConcatNode(nodeChecked, toAddArray);
                    if (nodeToAdd.isTreasure())
                    {
                        treasureFounded++;
                    }
                }
            }

            // Terminate if all treasure has been found
            if (treasureFounded < treasureLeft)
            {

                // Check down
                if (currentNode.getDown() != null)
                {
                    Node? nodeToAdd = currentNode.getDown();
                    currentNode.checkNode();
                    if (nodeToAdd?.getTimesChecked() == 0)
                    {
                        Node[] toAddArray = { nodeToAdd };
                        nodeChecked = ConcatNode(nodeChecked, toAddArray);
                        if (nodeToAdd.isTreasure())
                        {
                            treasureFounded++;
                        }
                    }
                }

                // Terminate if all treasure has been found
                if (treasureFounded < treasureLeft)
                {

                    // Check left
                    if (currentNode.getLeft() != null)
                    {
                        Node? nodeToAdd = currentNode.getLeft();
                        currentNode.checkNode();
                        if (nodeToAdd?.getTimesChecked() == 0)
                        {
                            Node[] toAddArray = { nodeToAdd };
                            nodeChecked = ConcatNode(nodeChecked, toAddArray);
                            if (nodeToAdd.isTreasure())
                            {
                                treasureFounded++;
                            }
                        }
                    }

                    // Terminate if all treasure has been found
                    if (treasureFounded < treasureLeft)
                    {

                        // Check up
                        if (currentNode.getUp() != null)
                        {
                            Node? nodeToAdd = currentNode.getUp();
                            currentNode.checkNode();
                            if (nodeToAdd?.getTimesChecked() == 0)
                            {
                                Node[] toAddArray = { nodeToAdd };
                                nodeChecked = ConcatNode(nodeChecked, toAddArray);
                                if (nodeToAdd.isTreasure())
                                {
                                    treasureFounded++;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Done checking treasure");

            // Check each child
            foreach (Node element in nodeChecked)
            {
                element.print();
            }

            var result = Tuple.Create<Node[], int>(nodeChecked, treasureFounded);
            return result;
        }

        // To check number of children (Udah approved)
        public static int CheckAnyChild(Node currentNode)
        {
            int numOfChild = 0;

            // Check right
            if (currentNode.getRight != null)
            {
                if (currentNode.getRight()?.getTimesChecked() == 0)
                {
                    numOfChild++;
                }
            }

            // Check down
            if (currentNode.getDown != null)
            {
                if (currentNode.getDown()?.getTimesChecked() == 0)
                {
                    numOfChild++;
                }
            }

            // Check left
            if (currentNode.getLeft != null)
            {
                if (currentNode.getLeft()?.getTimesChecked() == 0)
                {
                    numOfChild++;
                }
            }

            // Check up
            if (currentNode.getUp != null)
            {
                if (currentNode.getUp()?.getTimesChecked() == 0)
                {
                    numOfChild++;
                }
            }

            // Console.WriteLine("Done checking child");

            // Check each child
            return numOfChild;
        }

        // BFS

        public static Node[] BFSSearch(Node[,] map, int treasureNum)
        {
            int treasureFound = 0;
            Node[] searchedPath = { map[0, 0] };
            Node[] levelPath = { map[0, 0] };

            // Cari tiap node sampai ketemu semua node of treasure
            while (treasureFound < treasureNum)
            {
                // Array simpen node di level ke X
                Node[] levelX = { };

                // Check node di searchedPath
                foreach (Node element in searchedPath)
                {
                    int numOfChild = CheckAnyChild(element);

                    // Kalau punya anak cek status anaknya, masukin ke Node
                    if (numOfChild != 0)
                    {
                        Tuple<Node[], int> currentResult = CheckTreasure(element, treasureNum - treasureFound);
                        treasureFound += currentResult.Item2;
                        levelX = ConcatNode(levelX, currentResult.Item1);
                    }

                    if (treasureFound >= treasureNum)
                    {
                        break;
                    }

                }

                foreach (Node lelement in levelX)
                {
                    lelement.print();
                }

                Console.WriteLine("Berhasil Keluar Loop");

                // Masukin node array ke searchedPath dan concat ke levelPath
                searchedPath = levelX;
                levelPath = ConcatNode(levelPath, levelX); // Gatau ini push ya

                Console.WriteLine(searchedPath.Length);

                foreach (Node selement in searchedPath)
                {
                    selement.print();
                }
            }

            Console.WriteLine("Done doing BFS");

            return levelPath;
        }


        // DFS
        // Node to check {Udah sesuai}
        public static Node? NowChecking(Node currentNode)
        {
            Node? nodeToCheck = currentNode;

            // Check right
            if (currentNode.getRight != null)
            {
                if (currentNode.getRight()?.getTimesChecked() == 0)
                {
                    nodeToCheck = currentNode.getRight();
                }
            }

            // Check down
            if (nodeToCheck == currentNode && currentNode.getDown != null)
            {
                if (currentNode.getDown()?.getTimesChecked() == 0)
                {
                    nodeToCheck = currentNode.getDown();
                }
            }

            // Check left
            if (nodeToCheck == currentNode && currentNode.getLeft != null)
            {
                if (currentNode.getLeft()?.getTimesChecked() == 0)
                {
                    nodeToCheck = currentNode.getLeft();
                }
            }

            // Check up
            if (nodeToCheck == currentNode && currentNode.getUp != null)
            {
                if (currentNode.getUp()?.getTimesChecked() == 0)
                {
                    nodeToCheck = currentNode.getUp();
                }
            }

            return nodeToCheck;
        }

        // Recursive pattern
        public static Tuple<Node[], int> DFSSearchRecursive(Node[,] map, int treasureNum, Node currentNode, Node parentNode)
        {
            Node[] resultingPath = { };
            int treasureFound = 0;

            Console.WriteLine("Success checking current node");

            // Telusuri path sampai semua treasure ketemu
            if (treasureFound < treasureNum)
            {
                // Check in current node
                currentNode.checkNode();
                Node[] currentNodeArray = { currentNode };
                resultingPath = ConcatNode(resultingPath, currentNodeArray);
                if (currentNode.isTreasure())
                {
                    treasureFound++;
                }

                else
                {

                    // Cek kalau memiliki anak DFS in anak-anaknya
                    int childCheck = CheckAnyChild(currentNode) + 1;
                    while (currentNode.getTimesChecked() < childCheck)
                    {
                        Node? checkNode = NowChecking(currentNode);
                        if (checkNode != currentNode)
                        {
                            Tuple<Node[], int> childResult = DFSSearchRecursive(map, treasureNum - treasureFound, checkNode, currentNode);
                            treasureFound += childResult.Item2;
                            resultingPath = ConcatNode(resultingPath, childResult.Item1);
                            if (treasureFound >= treasureNum)
                            {
                                break;
                            }
                        }

                        currentNode.checkNode();
                        Node[] currentArray = { currentNode };
                        resultingPath = ConcatNode(resultingPath, currentArray);
                    }
                }
            }

            //Console.WriteLine("Success getting out from loop");

            Tuple<Node[], int> result = Tuple.Create<Node[], int>(resultingPath, treasureFound);
            return result;
        }

        public static Node[] DFSSearch(Node[,] map, int treasureNum)
        {
            Tuple<Node[], int> result = DFSSearchRecursive(map, treasureNum, map[0, 0], map[0, 0]);
            return result.Item1;
        }

        // TSP

        // Restart all nodes status of checking
        public static Node[,] RestartCheck(Node[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] != null)
                    {
                        map[i, j].restartCheck();
                    }
                }
            }

            return map;
        }

        public Node[] TSPSteps(Node start, Node finish)
        {
            start.checkNode();
            Node[] resultPath = { };
            Node[] currentNodeArray = { start };
            resultPath = ConcatNode(resultPath, currentNodeArray);
            if (start != finish)
            {
                int childCheck = CheckAnyChild(start) + 1;
                while (start.getTimesChecked() < childCheck)
                {
                    Node? checkNode = NowChecking(start);
                    if (checkNode != start && checkNode != null)
                    {
                        Node[] childResult = TSPSteps(checkNode, finish);
                        resultPath = ConcatNode(resultPath, childResult);

                    }
                    start.checkNode();
                    if (checkNode == finish)
                    {
                        for (int i = 0; i < childCheck - start.getTimesChecked(); i++)
                        {
                            start.checkNode();
                        }
                        break;
                    }
                }
            }

            return resultPath;
        }

        public Node[] TSPSearch(Node[,] map, int treasureNum)
        {
            // Search all treasure Nodes
            Node[] treasureNodes = { };
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] != null)
                    {
                        if (map[i, j].isTreasure() == true)
                        {
                            Node[] addElement = { map[i, j] };
                            treasureNodes = ConcatNode(treasureNodes, addElement);
                        }
                    }
                }
            }


            // Order treasure by closest distance
            Node[] orderedTreasure = { map[0, 0] };
            Node currentNode = map[0, 0];
            Node closestNode = treasureNodes[0];
            while (orderedTreasure.Length < treasureNum)
            {
                double min = currentNode.relativeDistance(treasureNodes[0]);
                foreach (Node element in treasureNodes)
                {
                    if (currentNode.relativeDistance(element) < min)
                    {
                        min = currentNode.relativeDistance(element);
                        closestNode = element;
                    }
                }
                Node[] toAdds = { closestNode };
                orderedTreasure = ConcatNode(orderedTreasure, toAdds);
                currentNode = closestNode;
                treasureNodes = treasureNodes.Where(e => e != currentNode).ToArray();
            }

            // Search step from each nodes
            Node[] tspPath = { };
            for (int i = 0; i < (orderedTreasure.Length - 1); i++)
            {
                Node[] result = TSPSteps(orderedTreasure[i], orderedTreasure[i + 1]);
                tspPath = ConcatNode(tspPath, result);
            }

            // Comeback step
            map = RestartCheck(map);
            Node[] toStart = TSPSteps(orderedTreasure[orderedTreasure.Length - 1], map[0, 0]);
            tspPath = ConcatNode(tspPath, toStart);

            tspPath = tspPath.Take(tspPath.Count() - 1).ToArray();

            return tspPath;

        }
    }
}

