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
    private int gridNumX = 9;

    /// <summary>
    /// y��������Ŀ
    /// </summary>
    private int gridNumY = 5;

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
                grid[i, j] = new Grid(new Vector2(i, j), 
                    new Vector2(transform.position.x + Grid.lengthX * i, 
                    transform.position.y + Grid.lengthY * j));
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
        //print("mousePos : " + mousePos + ", point : " + pointX + " " + pointY);

        // ����������򷵻�
        if (pointX >= 0 && pointX < gridNumX && pointY >= 0 && pointY < gridNumY)
        {
            return grid[pointX, pointY];
        }
        else return null;
    }
}
