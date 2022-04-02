using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class GridManager : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static GridManager Instance;

    /// <summary>
    /// x��������Ŀ
    /// </summary>
    public int gridNumX = 9;

    /// <summary>
    /// y��������Ŀ
    /// </summary>
    public int gridNumY = 5;

    /// <summary>
    /// ����
    /// </summary>
    public Grid[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new Grid[gridNumX, gridNumY];
        GenerateGrids();
    }

    /// <summary>
    /// ��������
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
    /// �õ�����Ŀ������������
    /// </summary>
    /// <returns>����</returns>
    public Grid GetNearestGrid(Vector3 pos)
    {
        // �����������������
        int pointX = (int)Mathf.Round((pos.x - transform.position.x) / Grid.lengthX);
        int pointY = (int)Mathf.Round((pos.y - transform.position.y) / Grid.lengthY);

        // ����������򷵻�
        if (pointX >= 0 && pointX < gridNumX && pointY >= 0 && pointY < gridNumY)
        {
            return grid[pointX, pointY];
        }
        else return null;
    }

    /// <summary>
    /// ��x����������x������
    /// </summary>
    /// <returns>x������</returns>
    public float GetPosX(int col)
    {
        return transform.position.x + Grid.lengthX * col;
    }

    /// <summary>
    /// ��y����������y������
    /// </summary>
    /// <returns>y������</returns>
    public float GetPosY(int row)
    {
        return transform.position.y + Grid.lengthY * row;
    }
}
