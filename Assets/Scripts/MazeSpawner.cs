using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public int mazeWidth;
    public int mazeHeight;
    
    public Transform cellsParent;
    public Transform deadZonesParent;
    
    public GameObject mazeCell;
    public GameObject deadZone;
    
    public Vector3 cellSize;
    
    private Maze _maze;

    private void Start()
    {
        MazeGenerator mazeGenerator = new MazeGenerator(mazeWidth, mazeHeight);
        _maze = mazeGenerator.GenerateMaze();
        
        for (int i = 0; i < _maze.cells.GetLength(0); i++)
        {
            for (int j = 0; j < _maze.cells.GetLength(1); j++)
            {
                MazeCell cell = Instantiate(mazeCell, new Vector3(i * cellSize.x, j * cellSize.y, j * cellSize.z), Quaternion.identity).GetComponent<MazeCell>();
                cell.transform.SetParent(cellsParent);

                if(i != 0 && j != 0 && i != _maze.cells.GetLength(0) - 1 && j != _maze.cells.GetLength(1) - 1)
                    if(Random.Range(0, 10) < 1)
                        Instantiate(deadZone, cell.deadZonePlace.position, Quaternion.identity).transform.SetParent(deadZonesParent);
                
                cell.westWall.SetActive(_maze.cells[i,j].westWall);
                cell.southWall.SetActive(_maze.cells[i,j].southWall);
            }
        }
    }
}
