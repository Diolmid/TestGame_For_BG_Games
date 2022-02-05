public class Maze
{
    public MazeGeneratorCell[,] cells;
}

public class MazeGeneratorCell
{
    public int x;
    public int y;

    public int distanceFromStart;
    
    public bool westWall = true;
    public bool southWall = true;

    public bool isVisited;
}
