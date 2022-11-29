namespace SearchAlgorithm
{
    public class BFSMazeSolver
    {
        /// <summary>
        /// Stores the shortest path from the starting point to the goal
        /// </summary>
        public List<Point> path = new List<Point>();

        /// <summary>
        /// Stores state and origin for backtracking from the goal back to the starting point for the shortest path
        /// </summary>
        private Dictionary<Point, Point> previous = new Dictionary<Point, Point>();

        public List<Point> Path
        {
            get { return path; } 
            set { path = value; }
        }

        /// <summary>
        /// Uses Breadth-First Search in finding the goal
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Shortest path distance from start to goal, -1 if goal can't be reached</returns>
        public int SolveMaze(string[,] matrix)
        {
            Point start = new Point(0, 0, 0);
            Queue<Point> queue = new Queue<Point>();

            int numOfRows = matrix.GetLength(0);
            int numOfColumns = matrix.GetLength(1);

            bool[,] visited = new bool[numOfRows, numOfColumns];

            // Set visited matrices to false;
            for (int x = 0; x < visited.GetLength(0); x += 1)
            {
                for (int y = 0; y < visited.GetLength(1); y += 1)
                {
                    visited[x, y] = false;
                }
            }

            queue.Enqueue(start);

            while (queue.Count() > 0)
            {
                Point current = queue.Dequeue();

                int index = matrix[current.x, current.y].IndexOf("G");
                if (index != -1)
                {
                    ShortestPath(start, current);
                    return current.shortestPathDistance;
                }
                else
                {
                    visited[current.x, current.y] = true;

                    List<Point> neighbors = FindNeighbors(current, numOfRows, numOfColumns, matrix, visited);
                    foreach(var neighbor in neighbors)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Locates neighbors of current point (UP,DOWN,RIGHT,LEFT) neigbhors
        /// </summary>
        /// <param name="current"> Current point(x,y) of matrix with distance</param>
        /// <param name="numOfRows">Number of rows of the matrix</param>
        /// <param name="numOfColumns">Number of columns of the matrix</param>
        /// <param name="matrix">2d array string matrix (maze)</param>
        /// <param name="visited">2d array bool matrix with visited points</param>
        /// <returns>List of Point(neighbors)</returns>
        private List<Point> FindNeighbors(Point current, int numOfRows, int numOfColumns, string[,] matrix, bool[,] visited)
        {
            List<Point> neighbors = new List<Point>();
            Point node;
            
            // Up
            if ((current.x - 1 >= 0 && current.x - 1 < numOfRows) && 
                !visited[current.x - 1, current.y] && 
                matrix[current.x - 1,current.y] != "▅")
            {
                node = new Point(current.x - 1, current.y, current.shortestPathDistance + 1);
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // Down
            if ((current.x + 1 >= 0 && current.x + 1 < numOfRows) && 
                !visited[current.x + 1, current.y] && 
                matrix[current.x + 1,current.y] != "▅")
            {
                node = (new Point(current.x + 1, current.y, current.shortestPathDistance + 1));
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // Right
            if ((current.y + 1 >= 0 && current.y + 1 < numOfColumns) &&
                !visited[current.x, current.y + 1] &&
                matrix[current.x, current.y + 1] != "▅")
            {
                node = new Point(current.x, current.y + 1, current.shortestPathDistance + 1);
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // Left
            if ((current.y - 1 >= 0 && current.y - 1 < numOfColumns) && 
                !visited[current.x, current.y - 1] && 
                matrix[current.x,current.y - 1] != "▅")
            {
                node = new Point(current.x, current.y - 1, current.shortestPathDistance + 1);
                neighbors.Add(node);
                previous.Add(node, current);
            }

            return neighbors;
        }

        /// <summary>
        /// Gets the shortest path from the starting point to the ending point
        /// Backtracking from the goal to the starting point
        /// </summary>
        /// <param name="start">Starting point of the maze</param>
        /// <param name="goal">The Goal that must be reached</param>
        public void ShortestPath(Point start, Point goal)
        {
            Point current = goal;
            while (current != start)
            {
                current = previous[current];
                path.Add(current);
            }
        }

        /// <summary>
        /// Displays the shortest path from start to goal
        /// </summary>
        /// <param name="matrix">2d array representation of the maze</param>
        public void DisplayShortestPath(string[,] matrix)
        {
            string shortestPath = "";
            int i = 0;
            foreach (var p in Path)
            {
                int indexS = matrix[p.x, p.y].IndexOf('S');
                if (indexS != -1)
                    matrix[p.x, p.y] = matrix[p.x, p.y].Replace('S', '\0');

                shortestPath += matrix[p.x, p.y];
                if (i < Path.Count() - 1)
                    shortestPath += " → ";
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPATH: \n" + shortestPath);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the maze
        /// </summary>
        /// <param name="matrix">2d array representation of the maze</param>
        public void DisplayMaze(string[,] matrix)
        {
            Console.WriteLine("\n\t\t\tGiven Maze\n");

            for (int x = 0; x < matrix.GetLength(0); x += 1)
            {
                for (int y = 0; y < matrix.GetLength(1); y += 1)
                {
                    int indexS = matrix[x, y].IndexOf("S");
                    int indexG = matrix[x, y].IndexOf("G");

                    if (matrix[x, y] == "▅")
                    {
                        Console.OutputEncoding = System.Text.Encoding.Unicode;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                        Console.ResetColor();

                    if (indexS != -1 || indexG != -1)
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write("\t" + matrix[x, y]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays maze showing the shortest path
        /// </summary>
        /// <param name="matrix">2d array representation of the maze</param>
        public void DisplayPathMaze(string[,] matrix)
        {
            Console.WriteLine("\n\t\tMaze with shortest path\n");

            string[,] pathMatrix = new string[matrix.GetLength(0), matrix.GetLength(1)];

            Point goal = new Point();

            // Copy matrix param to pathMatrix
            for (int x = 0; x < matrix.GetLength(0); x += 1)
            {
                for (int y = 0; y < matrix.GetLength(1); y += 1)
                {
                    int indexG = matrix[x, y].IndexOf("G");
                    if (indexG != -1)
                    {
                        goal.x = x;
                        goal.y = y;
                    }
                      
                    pathMatrix[x,y] = matrix[x, y];
                }
            }

            // Set up shortest path to matrix with equivalent arrows
            Path.Reverse();

            for(int i = 1; i < Path.Count(); i++)
            {
                Point curr = Path[i-1];
                Point next = Path[i];

                int indexS = pathMatrix[curr.x, curr.y].IndexOf("S");
                if (indexS != -1)
                    continue;

                if (next.x - curr.x < 0)
                    pathMatrix[curr.x, curr.y] = "↑";
                if (next.x - curr.x > 0)
                    pathMatrix[curr.x, curr.y] = "↓";
                if (next.y - curr.y < 0)
                    pathMatrix[curr.x, curr.y] = "←";
                if (next.y - curr.y > 0) 
                    pathMatrix[curr.x, curr.y] = "→";

                if (i == Path.Count() - 1)
                {
                    if (goal.x - next.x < 0)
                        pathMatrix[next.x, next.y] = "↑";
                    if (goal.x - next.x > 0)
                        pathMatrix[next.x, next.y] = "↓";
                    if (goal.y - next.y < 0)
                        pathMatrix[next.x, next.y] = "←";
                    if (goal.y - next.y > 0)
                        pathMatrix[next.x, next.y] = "→";
                }
            }

            // Print Matrix with shortest path
            for (int x = 0; x < pathMatrix.GetLength(0); x += 1)
            {
                for (int y = 0; y < pathMatrix.GetLength(1); y += 1)
                {
                    int indexS = pathMatrix[x, y].IndexOf("S");
                    int indexG = pathMatrix[x, y].IndexOf("G");

                    if (pathMatrix[x, y] == "↑" ||
                        pathMatrix[x, y] == "↓" ||
                        pathMatrix[x, y] == "←" ||
                        pathMatrix[x, y] == "→")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                        Console.ResetColor();

                    if (indexS != -1 || indexG != -1)
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    if (pathMatrix[x, y] == "▅")
                    {
                        Console.OutputEncoding = System.Text.Encoding.Unicode;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write("\t" + pathMatrix[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
