namespace Tubes2_Acetone
{
    public class InputMethods
    {
        // Hitung jumlah treasure
        public static int CountTreasure(string[,] map)
        {
            int treasureNum = 0;

            foreach (string element in map)
            {
                if (element == "T")
                {
                    treasureNum++;
                }
            }

            return treasureNum;
        }

        // TXT to node
        public static string[,] TXTtoArray(string filepath)
        {
            string[] fileRaw = File.ReadAllLines(filepath).ToArray();
            int n = fileRaw.Length;
            string[,] results = new string[n, n];
            int i = 0;
            foreach (string element in fileRaw)
            {
                string[] rows = element.Split(' ');
                for (int j = 0; j < rows.Length; j++)
                {
                    results[i, j] = rows[j];
                }
                i++;
            }
            return results;
        }

        // InputFile
        public static Node[,] InputFile(string FileName)
        {
            string[,] raw = TXTtoArray(FileName);
            Node[,] node = ArrToNode(raw);
            return node;
        }

        // Konversi dari array ke node
        public static Node[,] ArrToNode(string[,] map)
        {
            // Create node for every road and treasure array element
            int n = map.GetLength(0);
            Node[,] mapNode = new Node[n, n];
            mapNode[0, 0] = new Node(false, 1, 1);
            mapNode[0, 0].setStart();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (map[i, j] == "R")
                    {
                        mapNode[i, j] = new Node(false, i + 1, j + 1);
                    }
                    else if (map[i, j] == "T")
                    {
                        mapNode[i, j] = new Node(true, i + 1, j + 1);
                    }
                }
            }

            // Masukin ke branch
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mapNode[i, j] != null)
                    {
                        // Check left
                        if (j != 0)
                        {
                            if (mapNode[i, j - 1] != null)
                            {
                                mapNode[i, j].setLeft(mapNode[i, j - 1]);
                            }
                        }

                        // Check right
                        if (j != n - 1)
                        {
                            if (mapNode[i, j + 1] != null)
                            {
                                mapNode[i, j].setRight(mapNode[i, j + 1]);
                            }
                        }

                        // Check up
                        if (i != 0)
                        {
                            if (mapNode[i - 1, j] != null)
                            {
                                mapNode[i, j].setUp(mapNode[i - 1, j]);
                            }
                        }

                        // Check down
                        if (i != n - 1)
                        {
                            if (mapNode[i + 1, j] != null)
                            {
                                mapNode[i, j].setDown(mapNode[i + 1, j]);
                            }
                        }
                    }
                }
            }

            return mapNode;
        }
    }
}