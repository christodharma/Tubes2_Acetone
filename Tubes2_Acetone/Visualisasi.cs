namespace Tubes2_Acetone
{
    public class MapVisualizer
    {
        public static string[,] StringToVisualize(Node[,] map, Node[] checkedNode)
        {
            string[,] result = new string[map.GetLength(0), map.GetLength(1)];

            // Convert map to string
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (map[i, j] == null)
                    {
                        result[i, j] = "X";
                    }
                    else if (map[i, j].isTreasure())
                    {
                        result[i, j] = "T";
                    }
                    else if (map[i, j].isStart())
                    {
                        result[i, j] = "K";
                    }
                    else
                    {
                        result[i, j] = "R";
                    }
                }
            }

            // Tandain yang udah pernah dilewatin dan sudah dilewatin
            for (int i = 0; i < checkedNode.Length - 1; i++)
            {
                int a = checkedNode[i].getI() - 1;
                int b = checkedNode[i].getJ() - 1;

                if (checkedNode[i].isTreasure())
                {
                    result[a, b] = "C";
                }
                else
                {
                    result[a, b] = "A";
                }
            }

            // Tandain current node
            int x = checkedNode[checkedNode.Length - 1].getI() - 1;
            int y = checkedNode[checkedNode.Length - 1].getJ() - 1;
            result[x, y] = "B";

            return result;
        }

        public static void StepByStep(Node[,] map, Node[] checkedNode)
        {
            Node[] newChecked = { };
            for (int i = 0; i < checkedNode.Length; i++)
            {
                // Make current string map
                Node[] addCheck = { checkedNode[i] };
                newChecked = Algorithm.ConcatNode(newChecked, addCheck);
                string[,] currentHasil = StringToVisualize(map, newChecked);

                // Visualisasi map
                //for (int k = 0; k < currentHasil.GetLength(0); k++)
                //{
                //    for (int j = 0; j < currentHasil.GetLength(1); j++)
                //    {
                //        Console.Write(currentHasil[k, j]);
                //    }
                //    Console.WriteLine(" ");
                //}

                // Delay time
                Thread.Sleep(500);
            }
        }
    }
}