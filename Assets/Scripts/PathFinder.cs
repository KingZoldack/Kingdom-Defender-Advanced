using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoordinates;
    [SerializeField] Vector2Int _destinationCoordinates;

    Node _startNode;
    Node _destinationNode;
    Node _currentSearchNode;

    Queue<Node> _frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();
    
    Vector2Int[] _directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager _gridManager;
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        if (_gridManager != null)
        {
            _grid = _gridManager._Grid;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _startNode = _gridManager._Grid[_startCoordinates];
        _destinationNode = _gridManager._Grid[_destinationCoordinates];

        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    private void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        foreach (var direction in _directions)
        {
            Vector2Int neighbourCoords = _currentSearchNode.coordinates + direction;

            if (_grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(_grid[neighbourCoords]);
            }
        }

        foreach (var neighbour in neighbours)
        {
            if (!_reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable) 
            {
                neighbour.connectedTo = _currentSearchNode;
                _reached.Add(neighbour.coordinates, neighbour);
                _frontier.Enqueue(neighbour);
            }
        }
    }

    void BreadthFirstSearch()
    {
        _frontier.Clear();
        _reached.Clear();

        bool isRunning = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoordinates, _startNode);

        while (_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if (_currentSearchNode.coordinates == _destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].isWalkable;

            _grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            _grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }
}
