using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网格管理
/// </summary>
public class GridManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static GridManager Instance;

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

    private void Awake()
    {
        Instance = this;
        grid = new Grid[gridNumX, gridNumY];
        GenerateGrids();
    }

    /// <summary>
    /// 生成网格
    /// </summary>
    private void GenerateGrids()
    {
        for (int i = 0; i < gridNumX; i++)
        {
            for (int j = 0; j < gridNumY; j++)
            {
                grid[i, j] = new Grid(new Vector2(i, j), new Vector2(GetPosX(i), GetPosY(j)));
            }
        }
    }

    /// <summary>
    /// 得到距离目标点最近的网格
    /// </summary>
    /// <returns>网格</returns>
    public Grid GetNearestGrid(Vector3 pos)
    {
        // 计算最近的网格坐标
        int pointX = (int)Mathf.Round((pos.x - transform.position.x) / Grid.lengthX);
        int pointY = (int)Mathf.Round((pos.y - transform.position.y) / Grid.lengthY);

        // 若坐标符合则返回
        if (pointX >= 0 && pointX < gridNumX && pointY >= 0 && pointY < gridNumY)
        {
            return grid[pointX, pointY];
        }
        else return null;
    }

    /// <summary>
    /// 由x轴列数计算x轴坐标
    /// </summary>
    /// <returns>x轴坐标</returns>
    public float GetPosX(int col)
    {
        return transform.position.x + Grid.lengthX * col;
    }

    /// <summary>
    /// 由y轴行数计算y轴坐标
    /// </summary>
    /// <returns>y轴坐标</returns>
    public float GetPosY(int row)
    {
        return transform.position.y + Grid.lengthY * row;
    }
}
