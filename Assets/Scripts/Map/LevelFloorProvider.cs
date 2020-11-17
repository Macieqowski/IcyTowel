using UnityEngine;

public class LevelFloorProvider
{
    public Vector2 TileSize => _tileSize;
    public LevelFloorProvider(GameObject leftTile, GameObject middleTile, GameObject rightTile, GameObject leftWall, GameObject rightWall)
    {
        _leftTilePrefab = leftTile;
        _middleTilePrefab = middleTile;
        _rightTilePrefab = rightTile;
        _leftWallPrefab = leftWall;
        _rightWallPrefab = rightWall;

        _tileSize = _leftTilePrefab.GetComponent<BoxCollider2D>().size;
    }

    public GameObject CreateLevelFloor(Vector2 position, int size, float mapWidth)
    {
        var parentGO = new GameObject($"Floor {position.x}x{position.y}");
        parentGO.transform.position = position;

        GameObject.Instantiate(_leftWallPrefab, new Vector2(-(mapWidth / 2f), position.y), Quaternion.identity, parentGO.transform);
        GameObject.Instantiate(_rightWallPrefab, new Vector2(mapWidth / 2f, position.y), Quaternion.identity, parentGO.transform);

        if (size == 0)
        {
            return parentGO;
        }

        if (size < 2)
        {
            GameObject.Instantiate(_leftTilePrefab, position, Quaternion.identity, parentGO.transform);
            GameObject.Instantiate(_rightTilePrefab, position + new Vector2(_tileSize.x, 0f), Quaternion.identity, parentGO.transform);
            return parentGO;
        }

        var currentSpawnPosition = position;
        GameObject.Instantiate(_leftTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
        currentSpawnPosition.x += _tileSize.x;
        
        for (var i = 0; i < size - 2; i++)
        {
            GameObject.Instantiate(_middleTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
            currentSpawnPosition.x += _tileSize.x;
        }

        GameObject.Instantiate(_rightTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
        return parentGO;
    }

    

    private Vector2 _tileSize;

    private GameObject _leftTilePrefab;
    private GameObject _middleTilePrefab;
    private GameObject _rightTilePrefab;
    private GameObject _leftWallPrefab;
    private GameObject _rightWallPrefab;
}