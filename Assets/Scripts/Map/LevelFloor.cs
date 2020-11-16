using UnityEngine;
using System.Collections.Generic;

public class LevelFloor : MonoBehaviour
{
    public void CreateLevelFloor(Vector2 position, int size)
    {
        var parentGO = Instantiate(new GameObject($"Floor {position.x}x{position.y}"));
        if (size < 2)
        {
            Instantiate(_leftTilePrefab, position, Quaternion.identity, parentGO.transform);
            Instantiate(_rightTilePrefab, position + _leftTileSize, Quaternion.identity, parentGO.transform);
            _floors.Add(parentGO);
            return;
        }

        var currentSpawnPosition = position;
        Instantiate(_leftTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
        currentSpawnPosition += _leftTileSize;
        
        for (var i = 0; i < size - 2; i++)
        {
            Instantiate(_middleTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
            currentSpawnPosition += _middleTileSize;
        }

        Instantiate(_rightTilePrefab, currentSpawnPosition, Quaternion.identity, parentGO.transform);
        _floors.Add(parentGO);
    }

    public void DestroyFloorUnder(Vector2 position)
    {
        for (var i = _floors.Count; i >= 0; i--)
        {
            if (_floors[i].transform.position.y < position.y)
            {
                Destroy(_floors[i]);
            }
        }
    }
    protected void Awake()
    {
        _leftTileSize = _leftTilePrefab.GetComponent<BoxCollider2D>().size;
        _middleTileSize = _middleTilePrefab.GetComponent<BoxCollider2D>().size;
        _rightTileSize = _rightTilePrefab.GetComponent<BoxCollider2D>().size;
    }

    [SerializeField]
    private GameObject _leftTilePrefab = default;

    [SerializeField]
    private GameObject _middleTilePrefab = default;

    [SerializeField]
    private GameObject _rightTilePrefab = default;

    private Vector2 _leftTileSize;
    private Vector2 _middleTileSize;
    private Vector2 _rightTileSize;
    private List<GameObject> _floors = new List<GameObject>();
}