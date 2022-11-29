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
    }
}
