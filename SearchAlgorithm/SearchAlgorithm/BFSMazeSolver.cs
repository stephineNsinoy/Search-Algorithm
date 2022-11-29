using System.Security.Cryptography.X509Certificates;

namespace SearchAlgorithm
{
    public class BFSMazeSolver
    {
        /// <summary>
        /// Stores the shortest path from the starting point to the goal
        /// </summary>
        public List<Point> path = new List<Point>();

        /// <summary>
        /// Stores state and origin for backtracking from the goal back to the starting point with the shortest path
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

            // set visited matrices to false;
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
                    DisplayMaze(matrix);
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

            DisplayMaze(matrix);
            return -1;
        }

        /// <summary>
        /// Locates neighbors of current point (UP,DOWN,RIGHT,LEFT) neigbhors
        /// </summary>
        /// <param name="current"> current point(x,y) of matrix with distance</param>
        /// <param name="numOfRows">Number of rows of the matrix</param>
        /// <param name="numOfColumns">Number of columns of the matrix</param>
        /// <param name="matrix">2d array string matrix (maze)</param>
        /// <param name="visited">2d array bool matrix with visited points</param>
        /// <returns>List of Point(neighbors)</returns>
        private List<Point> FindNeighbors(Point current, int numOfRows, int numOfColumns, string[,] matrix, bool[,] visited)
        {

            List<Point> neighbors = new List<Point>();
            Point node;
            
            // UP
            if ((current.x - 1 >= 0 && current.x - 1 < numOfRows) && 
                !visited[current.x - 1, current.y] && 
                matrix[current.x - 1,current.y] != "▅")
            {
                node = new Point(current.x - 1, current.y, current.shortestPathDistance + 1);
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // DOWN
            if ((current.x + 1 >= 0 && current.x + 1 < numOfRows) && 
                !visited[current.x + 1, current.y] && 
                matrix[current.x + 1,current.y] != "▅")
            {
                node = (new Point(current.x + 1, current.y, current.shortestPathDistance + 1));
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // RIGHT
            if ((current.y + 1 >= 0 && current.y + 1 < numOfColumns) &&
                !visited[current.x, current.y + 1] &&
                matrix[current.x, current.y + 1] != "▅")
            {
                node = new Point(current.x, current.y + 1, current.shortestPathDistance + 1);
                neighbors.Add(node);
                previous.Add(node, current);
            }

            // LEFT
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
    
    }
}
