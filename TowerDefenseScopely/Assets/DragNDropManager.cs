using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragNDropManager : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;

    private Vector3Int? lastHighlightedCell = null;

    public Color highlightColor = Color.yellow;

    public Color originalColor = Color.white;

    public List<GameObject> torretas;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(cellPos);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = tilemap.CellToWorld(cellPos);
            Instantiate(torretas[0], spawnPos, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPos = tilemap.CellToWorld(cellPos);
            Instantiate(torretas[1], spawnPos, Quaternion.identity);
        }



        if (tile != null)
        {
            Debug.Log("Mouse over tile at cell " + cellPos + " : " + tile.name);

            if (!lastHighlightedCell.HasValue || lastHighlightedCell.Value != cellPos)
            {
                if (lastHighlightedCell.HasValue)
                    tilemap.SetColor(lastHighlightedCell.Value, originalColor);

                tilemap.SetColor(cellPos, highlightColor);
                lastHighlightedCell = cellPos;
            }
        }
        else
        {
            Debug.Log("Mouse over no tile at cell " + cellPos);

            if (lastHighlightedCell.HasValue)
            {
                tilemap.SetColor(lastHighlightedCell.Value, originalColor);
                lastHighlightedCell = null;
            }
        }
    }
}

