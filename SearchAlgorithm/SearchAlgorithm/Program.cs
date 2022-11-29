using SearchAlgorithm;

string[,] matrix = 
        {
                {"1S", "2", "3", "▅", "5", "6" },
                {"▅", "8", "▅", "10", "11", "12"},
                {"13", "14", "▅", "16", "▅", "18"},
                {"19", "▅", "21", "22", "▅", "24"},
                {"25", "26", "27", "▅", "29G", "30"},
                {"31", "▅", "33", "▅", "35", "36"},
        };


BFSMazeSolver solver = new BFSMazeSolver();

int distance = solver.SolveMaze(matrix);
if(distance != -1)
{
    solver.DisplayMaze(matrix);
    solver.DisplayPathMaze(matrix);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\nGoal reached with distance {distance} from start to goal");
    solver.DisplayShortestPath(matrix);
}
else
{
    solver.DisplayMaze(matrix);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nGoal can't be reached!\nMaze is impossible to solve");
    Console.ResetColor();   
}