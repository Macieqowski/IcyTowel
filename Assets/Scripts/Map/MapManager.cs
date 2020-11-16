using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public void Inject(Player player)
    {
        _player = player;
    }

    public void CreateMap()
    {
        while (_floorObjects.Count > 0)
        {
            Destroy(_floorObjects.Dequeue());
        }
        Destroy(_wallColliders[0]);
        Destroy(_wallColliders[1]);
        _currentGenerationYPosition = 0f;
        _lastLevelWithFloorYPosition = 0f;

        var floorLevelGO = _levelFloorProvider.CreateLevelFloor(new Vector2(-MapWidthInUnits / 2f, 0f), _mapWidth, MapWidthInUnits);
        CreateWallColliders();
        _floorObjects.Enqueue(floorLevelGO);
        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _levelFloorProvider.TileSize.y);
        _currentGenerationYPosition += _levelFloorProvider.TileSize.y;
        _lastLevelWithFloorYPosition = _currentGenerationYPosition;
        _isMapCreated = true;
    }

    protected void Awake()
    {
        _levelFloorProvider = new LevelFloorProvider(_leftTilePrefab, _middleTilePrefab, _rightTilePrefab, _leftWallPrefab, _rightWallPrefab);
        _floorObjects = new Queue<GameObject>((int)(_renderDistance * _levelFloorProvider.TileSize.y));
    }


    protected void Update()
    {
        if (!_isMapCreated)
        {
            return;
        }

        if (_currentGenerationYPosition < _player.transform.position.y + _renderDistance)
        {
            ExtendWallColliders();

            if (_currentGenerationYPosition - _lastLevelWithFloorYPosition < _floorSpacing)
            {
                _floorObjects.Enqueue(_levelFloorProvider.CreateLevelFloor(new Vector2(0f, _currentGenerationYPosition), 0, MapWidthInUnits));
                _currentGenerationYPosition += _levelFloorProvider.TileSize.y;
                return;
            }

            var floorWidth = Random.Range(2, _mapWidth - 3);
            var position = (-_mapWidth / 2f + floorWidth / 2f) * _levelFloorProvider.TileSize.x;

            _floorObjects.Enqueue(_levelFloorProvider.CreateLevelFloor(new Vector2(position, _currentGenerationYPosition), floorWidth, MapWidthInUnits));
            _currentGenerationYPosition += _levelFloorProvider.TileSize.y;
            _lastLevelWithFloorYPosition = _currentGenerationYPosition;
        }

        if (_floorObjects.Count > 2 * _renderDistance)
        {
            Destroy(_floorObjects.Dequeue());
            ShrinkWallColliders();
        }
    }

    [SerializeField]
    private GameObject _leftTilePrefab = default;

    [SerializeField]
    private GameObject _middleTilePrefab = default;

    [SerializeField]
    private GameObject _rightTilePrefab = default;

    [SerializeField]
    private GameObject _leftWallPrefab = default;

    [SerializeField]
    private GameObject _rightWallPrefab = default;

    [SerializeField]
    private PhysicsMaterial2D _wallsMaterial = default;

    [SerializeField]
    private GameObject _backgroundPrefab = default;

    [SerializeField]
    private int _mapWidth = default;

    [SerializeField]
    private float _renderDistance = default;

    [SerializeField]
    private float _floorSpacing = default;

    private float MapWidthInUnits => _mapWidth * _levelFloorProvider.TileSize.x;

    private float _currentGenerationYPosition = 0f;
    private float _lastLevelWithFloorYPosition = 0f;

    private Queue<GameObject> _floorObjects;
    private LevelFloorProvider _levelFloorProvider;
    private Player _player;
    private BoxCollider2D[] _wallColliders = new BoxCollider2D[2];
    private bool _isMapCreated;

    private void CreateWallColliders()
    {
        _wallColliders[0] = gameObject.AddComponent<BoxCollider2D>();
        _wallColliders[0].offset = new Vector2(-_mapWidth / 2f, 0f);
        _wallColliders[0].size = _levelFloorProvider.TileSize;
        _wallColliders[0].sharedMaterial = _wallsMaterial;

        _wallColliders[1] = gameObject.AddComponent<BoxCollider2D>();
        _wallColliders[1].offset = new Vector2(_mapWidth / 2f, 0f);
        _wallColliders[1].size = _levelFloorProvider.TileSize;
        _wallColliders[1].sharedMaterial = _wallsMaterial;
    }

    private void ExtendWallColliders()
    {
        _wallColliders[0].offset = new Vector2(_wallColliders[0].offset.x, _wallColliders[0].offset.y + _levelFloorProvider.TileSize.y / 2f);
        _wallColliders[0].size = new Vector2(_wallColliders[0].size.x, _wallColliders[0].size.y + _levelFloorProvider.TileSize.y);

        _wallColliders[1].offset = new Vector2(_wallColliders[1].offset.x, _wallColliders[1].offset.y + _levelFloorProvider.TileSize.y / 2f);
        _wallColliders[1].size = new Vector2(_wallColliders[1].size.x, _wallColliders[1].size.y + _levelFloorProvider.TileSize.y);
    }

    private void ShrinkWallColliders()
    {
        _wallColliders[0].offset = new Vector2(_wallColliders[0].offset.x, _wallColliders[0].offset.y + _levelFloorProvider.TileSize.y / 2f);
        _wallColliders[0].size = new Vector2(_wallColliders[0].size.x, _wallColliders[0].size.y - _levelFloorProvider.TileSize.y);

        _wallColliders[1].offset = new Vector2(_wallColliders[1].offset.x, _wallColliders[1].offset.y + _levelFloorProvider.TileSize.y / 2f);
        _wallColliders[1].size = new Vector2(_wallColliders[1].size.x, _wallColliders[1].size.y - _levelFloorProvider.TileSize.y);
    }
}
