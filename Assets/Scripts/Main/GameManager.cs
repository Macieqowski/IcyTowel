using UnityEngine;
using System.Collections;


public class GameManager
{
    public GameManager(Player player, CameraController cameraController, MapManager mapManager, AssetManager assetManager, InputManager inputManager)
    {
        _player = player;
        _cameraController = cameraController;
        _mapManager = mapManager;
        _assetManager = assetManager;
        _inputManager = inputManager;
    }

    public void OnUpdate(float deltaTime)
    {
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

    private const float IgnorableValue = 0.001f;
    private const KeyCode JumpButton = KeyCode.Space;

    private Player _player;
    private CameraController _cameraController;
    private MapManager _mapManager;
    private AssetManager _assetManager;
    private InputManager _inputManager;
}
