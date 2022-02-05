using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    private int width;
    private int height;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    
    public Maze GenerateMaze()
    {
        var cells = new MazeGeneratorCell[width, height];

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                cells[i, j] = new MazeGeneratorCell { x = i, y = j };
            }
        }

        for (int i = 0; i < cells.GetLength(0); i++)
            cells[i, height - 1].westWall = false;
        
        for (int j = 0; j < cells.GetLength(1); j++)
            cells[width - 1, j].southWall = false;

        RemoveWalls(cells);

        Maze maze = new Maze();
        maze.cells = cells;
        
        return maze;
    }

    private void RemoveWalls(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        var stack = new Stack<MazeGeneratorCell>();

        current.isVisited = true;
        current.distanceFromStart = 0;
        
        do
        {
            var unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.x;
            int y = current.y;
            
            if(x > 0 && !maze[x - 1, y].isVisited)
                unvisitedNeighbours.Add(maze[x - 1, y]);
            
            if(y > 0 && !maze[x, y - 1].isVisited)
                unvisitedNeighbours.Add(maze[x, y - 1]);
            
            if(x < width - 2 && !maze[x + 1, y].isVisited)
                unvisitedNeighbours.Add(maze[x + 1, y]);
            
            if(y < height - 2 && !maze[x, y + 1].isVisited)
                unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);
                
                chosen.isVisited = true;
                current = chosen;
                stack.Push(chosen);
                chosen.distanceFromStart = current.distanceFromStart + 1;
            }
            else
            {
                current = stack.Pop();
            }
            
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.x == b.x)
        {
            if (a.y > b.y)
                a.southWall = false;
            else
                b.southWall = false;
        }
        else
        {
            if (a.x > b.x)
                a.westWall = false;
            else
                b.westWall = false;
        }
    }
}
