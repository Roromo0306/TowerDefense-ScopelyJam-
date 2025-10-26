using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DragNDropManager : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;

    public Canvas canvas;
    public ElixirBar elixir;
    private Vector3Int? lastHighlightedCell = null;

    public Color highlightColor = Color.yellow;
    public Color originalColor = Color.white;

    public List<GameObject> torretas;

    // Coste por ejemplo (opcional)
    public int elixirCost = 2;

    private ElixirBar elixirBar;

    void Start()
    {
        if (canvas != null)
        {
            elixirBar = canvas.GetComponent<ElixirBar>();
            if (elixirBar == null)
                Debug.LogWarning("No se encontró ElixirBar en el Canvas asignado.");
        }
        else
        {
            Debug.LogWarning("Canvas no asignado en DragNDropManager.");
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(cellPos);
        if(elixir.currentElixir > 3)
        {
            // Click izquierdo: torreta 0
            if (Input.GetMouseButtonDown(0))
            {
                // Encontrar la celda central (si existe) en un radio de 1 (3x3)
                Vector3Int centerCell = FindCenterTileCell(worldPos, cellPos, 1);
                Vector3 spawnPos = tilemap.GetCellCenterWorld(centerCell);

                if (elixirBar != null)
                    elixirBar.currentElixir -= elixirCost;

                if (torretas != null && torretas.Count > 0 && torretas[0] != null)
                    Instantiate(torretas[0], spawnPos, torretas[0].transform.rotation);
                else
                    Debug.LogWarning("Torreta 0 no asignada en la lista 'torretas'.");
            }

            // Click derecho: torreta 1
            if (Input.GetMouseButtonDown(1))
            {
                Vector3Int centerCell = FindCenterTileCell(worldPos, cellPos, 1);
                Vector3 spawnPos = tilemap.GetCellCenterWorld(centerCell);

                if (elixirBar != null)
                    elixirBar.currentElixir -= elixirCost;

                if (torretas != null && torretas.Count > 1 && torretas[1] != null)
                    Instantiate(torretas[1], spawnPos, torretas[1].transform.rotation);
                else
                    Debug.LogWarning("Torreta 1 no asignada en la lista 'torretas'.");
            }

        }

        // Highlight del tile bajo el ratón (igual que antes)
        if (tile != null)
        {
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
            if (lastHighlightedCell.HasValue)
            {
                tilemap.SetColor(lastHighlightedCell.Value, originalColor);
                lastHighlightedCell = null;
            }
        }
    }

    // Busca en un radio alrededor de clickedCell una celda que tenga TileBase asignado.
    // Si la encuentra, devuelve esa celda (asumimos que será la central del sprite 3x3).
    // Si no, devuelve la celda clicada.
    private Vector3Int FindCenterTileCell(Vector3 worldPos, Vector3Int clickedCell, int searchRadius = 1)
    {
        for (int y = -searchRadius; y <= searchRadius; y++)
        {
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                Vector3Int c = new Vector3Int(clickedCell.x + x, clickedCell.y + y, clickedCell.z);
                TileBase t = tilemap.GetTile(c);
                if (t != null)
                {
                    // Opción: comprobar bounds del sprite para mayor precisión (descomentarlo si lo necesitas)
                    /*
                    Tile tileObj = t as Tile;
                    if (tileObj != null && tileObj.sprite != null)
                    {
                        Vector3 spriteSize = tileObj.sprite.bounds.size; // tamaño en unidades mundo
                        Vector3 centerWorld = tilemap.GetCellCenterWorld(c);
                        Rect spriteRect = new Rect(
                            centerWorld.x - spriteSize.x / 2f,
                            centerWorld.y - spriteSize.y / 2f,
                            spriteSize.x,
                            spriteSize.y
                        );
                        if (spriteRect.Contains(new Vector2(worldPos.x, worldPos.y)))
                            return c;
                    }
                    else
                    {
                        return c;
                    }
                    */
                    return c;
                }
            }
        }
        return clickedCell;
    }
}


