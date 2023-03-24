namespace Tubes2_Acetone
{
    public class Node
    {
        // Attribut
        int i;
        int j;
        bool startStatus;
        bool treasureStatus;
        int timesChecked;
        Node? left;
        Node? right;
        Node? up;
        Node? down;

        // Constructor
        public Node(bool treasureStatus, int i, int j)
        {
            this.i = i;
            this.j = j;
            this.treasureStatus = treasureStatus;
            this.timesChecked = 0;
            this.left = null;
            this.right = null;
            this.up = null;
            this.down = null;
            this.startStatus = false;
        }

        // Getter
        public int getI()
        {
            return this.i;
        }

        public int getJ()
        {
            return this.j;
        }
        public string getPosition()
        {
            return "(" + this.i + "," + this.j + ")";
        }

        public Node? getLeft()
        {
            return this.left;
        }

        public Node? getRight()
        {
            return this.right;
        }

        public Node? getUp()
        {
            return this.up;
        }

        public Node? getDown()
        {
            return this.down;
        }

        public bool isChecked()
        {
            return this.timesChecked > 0;
        }

        public int getTimesChecked()
        {
            return this.timesChecked;
        }

        public bool isTreasure()
        {
            return this.treasureStatus;
        }

        public bool isStart()
        {
            return this.startStatus;
        }

        // Setter
        public void setLeft(Node newleft)
        {
            this.left = newleft;
        }

        public void setRight(Node newright)
        {
            this.right = newright;
        }

        public void setUp(Node newup)
        {
            this.up = newup;
        }

        public void setDown(Node newdown)
        {
            this.down = newdown;
        }

        public void checkNode()
        {
            this.timesChecked++;
        }

        public void setStart()
        {
            this.startStatus = true;
        }

        public void restartCheck()
        {
            this.timesChecked = 0;
        }
        // Method
        public void print()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.Write("This node is in postition" + this.getPosition() + " This is a ");
            if (this.startStatus)
            {
                Console.WriteLine("Start");
            }
            else
            {
                if (this.treasureStatus)
                {
                    Console.WriteLine("Treasure");
                }
                else
                {
                    Console.WriteLine("Road");
                }
            }
            if (this.left != null)
            {
                Console.WriteLine("Left: " + left.isTreasure());
            }

            if (this.right != null)
            {
                Console.WriteLine("Right: " + right.isTreasure());
            }

            if (this.up != null)
            {
                Console.WriteLine("Up: " + up.isTreasure());
            }

            if (this.down != null)
            {
                Console.WriteLine("Down: " + down.isTreasure());
            }
            Console.WriteLine("--------------------------------------------------");
        }

        public double relativeDistance(Node x)
        {
            double distance = Math.Sqrt((this.i - x.i) * (this.i - x.i) + (this.j - x.j) * (this.j - x.j));
            return distance;
        }

        // Main check
        //public static void Main(String[] args) {

        //}
    }
}
