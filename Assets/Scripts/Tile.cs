using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower _towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager _gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                _gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = _towerPrefab.CreateTower(_towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
