using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网格管理
/// </summary>
public class GridManager : MonoBehaviour
{
    /// <summary>
    /// x轴网格数目
    /// </summary>
    public int gridNumX = 9;

    /// <summary>
    /// y轴网格数目
    /// </summary>
    public int gridNumY = 5;

    /// <summary>
    /// 网格
    /// </summary>
    public Grid[,] grid;



    private void Start()
    {
        grid = new Grid[gridNumX, gridNumY];
    }

    private void GenerateGrids()
    {
        for (int i = 0; i < gridNumX; i++)
        {
            for (int j = 0; j < gridNumY; j++)
            {
                grid[i, j] = new Grid(new Vector2(i, j), 
                    new Vector2(transform.position.x + Grid.lengthX * i, 
                    transform.position.y + Grid.lengthY * j));
            }
        }
    }
}
