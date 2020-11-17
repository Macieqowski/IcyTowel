using UnityEngine;

public class InputManager
{
    public InputManager()
    {

    }

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool GetKeyPressed(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool GetKeyPressed(string key)
    {
        return Input.GetButtonDown(key);
    }
}