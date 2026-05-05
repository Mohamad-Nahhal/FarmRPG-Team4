using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tilemap _groundTilemap;
    [SerializeField] private Tilemap _collisionTilemap;

    private Dictionary<Vector2Int, TileData> _tiles = new Dictionary<Vector2Int, TileData>();

    private void Awake()
    {
        InitializeTiles();
    }

    private void InitializeTiles()
{
    BoundsInt bounds = _groundTilemap.cellBounds;

    foreach (Vector3Int pos in bounds.allPositionsWithin)
    {
        if (!_groundTilemap.HasTile(pos))
            continue;

        Vector2Int gridPos = new Vector2Int(pos.x, pos.y);

        TileBase unityTile = _groundTilemap.GetTile(pos);

        TileData tile = new TileData
        {
            Type = TileType.Grass,
            IsWalkable = true,
            IsInteractable = false
        };

        if (unityTile != null && unityTile.name.Contains("Door"))
        {
            tile.Type = TileType.Door;
            tile.IsInteractable = true;

            if (unityTile.name.Contains("DoorHouse1"))
            {
                tile.DoorID = "DoorHouse1";
                tile.TargetScene = "01_Farm.scene";
                tile.SpawnPosition = new Vector2(71, 0);
            }

            if (unityTile.name.Contains("DoorExit1"))
            {
                tile.DoorID = "DoorExit1";
                tile.TargetScene = "01_Farm.scene";
                tile.SpawnPosition = new Vector2(-12, -1);
            }

             if (unityTile.name.Contains("DoorHouse2"))
            {
                tile.DoorID = "DoorHouse2";
                tile.TargetScene = "01_Farm.scene";
                tile.SpawnPosition = new Vector2(116, 0);
            }

             if (unityTile.name.Contains("DoorExit2"))
            {
                tile.DoorID = "DoorExit2";
                tile.TargetScene = "01_Farm.scene";
                tile.SpawnPosition = new Vector2(12, 0);
            }
        }

        _tiles[gridPos] = tile;
    }
}
    

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        Vector3Int cell = _groundTilemap.WorldToCell(worldPosition);
        return new Vector2Int(cell.x, cell.y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        // using CellToWorld (bottom-left), so we offset later
        return _groundTilemap.CellToWorld((Vector3Int)gridPosition);
    }

    public TileData GetTile(Vector2Int position)
    {
        _tiles.TryGetValue(position, out TileData tile);
        return tile;
    }
}