using UnityEngine;

public class InputManager
{
    public InputManager()
    {

    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool GetKeyPressed(string key)
    {
        return Input.GetButtonDown(key);
    }
}