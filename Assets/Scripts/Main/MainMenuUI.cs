using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public Action OnNewGame;
    public Action OnExit;
    public void NewGame() => OnNewGame?.Invoke();
    public void Exit() => OnExit?.Invoke();
    public void SetActive(bool active) => gameObject.SetActive(active);
}
