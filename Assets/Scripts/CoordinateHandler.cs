using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateHandler : MonoBehaviour
{
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.red;
    [SerializeField] Color _exploredColor = Color.black;
    [SerializeField] Color _pathColor = Color.green;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager _grinManager;

    private void Awake()
    {
        _grinManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void SetLabelColor()
    {
        if(_grinManager == null) { return; }

        Node node = _grinManager.GetNode(coordinates);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = _blockedColor;
        }

        else if (node.isPath)
        {
            label.color = _pathColor;
        }

        else if (node.isExplored)
        {
            label.color = _exploredColor;
        }

        else
        {
            label.color = _defaultColor;
        }
    }

    void DisplayCoordinates()
     {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
     }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
