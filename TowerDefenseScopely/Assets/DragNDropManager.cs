using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragNDropManager : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;

    // Guarda la celda resaltada previamente
    private Vector3Int? lastHighlightedCell = null;

    // Color de resaltado
    public Color highlightColor = Color.yellow;

    // Color original (si usas uno global)
    public Color originalColor = Color.white;

    void Update()
    {
        // Convertir posición del ratón a mundo
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        // No es necesario raycast para sólo detección del tilemap celda
        // Si quieres usar raycast por fuerza, podrías, pero aquí lo simplifico:
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(cellPos);

        if (tile != null)
        {
            Debug.Log("Mouse over tile at cell " + cellPos + " : " + tile.name);

            // Si la celda es diferente a la anterior, actualizamos resaltado
            if (!lastHighlightedCell.HasValue || lastHighlightedCell.Value != cellPos)
            {
                // Desresaltar la anterior
                if (lastHighlightedCell.HasValue)
                {
                    tilemap.SetColor(lastHighlightedCell.Value, originalColor);
                }

                // Resaltar la nueva
                tilemap.SetColor(cellPos, highlightColor);
                lastHighlightedCell = cellPos;
            }
        }
        else
        {
            Debug.Log("Mouse over no tile at cell " + cellPos);

            // Si ya había una celda resaltada, quitar resaltado
            if (lastHighlightedCell.HasValue)
            {
                tilemap.SetColor(lastHighlightedCell.Value, originalColor);
                lastHighlightedCell = null;
            }
        }
    }
}
