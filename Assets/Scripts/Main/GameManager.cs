using UnityEngine;

public class GameManager
{
    public GameManager(Player player, CameraController cameraController, MapManager mapManager, AssetManager assetManager, InputManager inputManager, UIManager uiManager)
    {
        _player = player;
        _cameraController = cameraController;
        _mapManager = mapManager;
        _assetManager = assetManager;
        _inputManager = inputManager;
        _uiManager = uiManager;

        _uiManager.OnNewGame += StartNewGame;
        _uiManager.OnExit += Exit;
    }

    public void OnUpdate(float deltaTime)
    {
        if (_isGameStarted)
        {
            if (_player.IsDead)
            {
                _isGameStarted = false;
                _uiManager.EnterMenu();
            }
            var movement = _inputManager.GetHorizontalAxis();
            if (Mathf.Abs(movement) > IgnorableValue)
            {
                _player.Move(movement);
            }

            if (_inputManager.GetKeyPressed(JumpButton))
            {
                _player.Jump();
            }
        }
    }

    public void OnDestroy()
    {
        _uiManager.OnNewGame -= StartNewGame;
        _uiManager.OnExit -= Exit;
    }

    private const float IgnorableValue = 0.001f;
    private const KeyCode JumpButton = KeyCode.Space;

    private Player _player;
    private CameraController _cameraController;
    private MapManager _mapManager;
    private AssetManager _assetManager;
    private InputManager _inputManager;
    private UIManager _uiManager;
    private bool _isGameStarted;

    private void StartNewGame()
    {
        _player.transform.position = new Vector2(0f, 0f);
        _mapManager.CreateMap();
        _isGameStarted = true;
        _uiManager.EnterPlaymode();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
