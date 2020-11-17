using UnityEngine;

public class Starter : MonoBehaviour
{
    protected void Awake()
    {
        DontDestroyOnLoad(this);
        _assetManager = new AssetManager();
        _player = Instantiate(_playerPrefab).GetComponent<Player>();
        _cameraController = Instantiate(_cameraPrefab).GetComponent<CameraController>();
        _cameraController.InjectTransformToFollow(_player.transform);
        _mapManager = Instantiate(_mapManagerPrefab).GetComponent<MapManager>();
        _mapManager.Inject(_player);
        _inputManager = new InputManager();
        _uiManager = Instantiate(_uiManagerPrefab).GetComponent<UIManager>();
        _gameManager = new GameManager(_player, _cameraController, _mapManager, _assetManager, _inputManager, _uiManager);
    }

    protected void Update()
    {
        _gameManager.OnUpdate();
    }
    protected void OnDestroy()
    {
        _gameManager.OnDestroy();
    }

    [SerializeField]
    private GameObject _playerPrefab = default;

    [SerializeField]
    private GameObject _cameraPrefab = default;

    [SerializeField]
    private GameObject _mapManagerPrefab = default;

    [SerializeField]
    private GameObject _uiManagerPrefab = default;

    private AssetManager _assetManager;
    private Player _player;
    private CameraController _cameraController;
    private MapManager _mapManager;
    private InputManager _inputManager;
    private GameManager _gameManager;
    private UIManager _uiManager;
}
