namespace SearchAlgorithm
{
    public class Point
    {
       public int x { get; set; }
       public int y { get; set; }
       public int shortestPathDistance { get; set; }

        public Point() { }
       public Point(int x, int y, int distance)
       {
            this.x = x;
            this.y = y;
            this.shortestPathDistance = distance;
       }
    }
}
