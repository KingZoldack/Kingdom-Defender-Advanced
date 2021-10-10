using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int _gridSize;

    [Tooltip("Unity Grid Size - Should match UnityEditor snap setting.")]
    [SerializeField] int _unityGridSize = 10;
    public int UnityGridSize { get { return _unityGridSize; } }

    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> _Grid { get { return _grid; } }

    private void Awake()
    {
        CreatGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            return _grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            _grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in _grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / _unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / _unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * _unityGridSize;
        position.z = coordinates.y * _unityGridSize;

        return position;
    }

    private void CreatGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                _grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
