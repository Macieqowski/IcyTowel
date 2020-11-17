using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Action OnNewGame;
    public Action OnExit;

    public int DistanceTravelled
    {
        set => _hud.DistanceTravelled = $"{value}";
    }
    public float TimeElapsed 
    {
        set => _hud.TimeElapsed = $"{((int)(value / 60))}:{(value % 60).ToString("F2")}";
    }

    public void NewGame() => OnNewGame?.Invoke();
    public void Exit() => OnExit?.Invoke();

    public void EnterPlaymode()
    {
        _mainMenu.SetActive(false);
        _hud.SetActive(true);
    }

    public void EnterMenu()
    {
        _mainMenu.SetActive(true);
        _hud.SetActive(false);
    }

    protected void Awake()
    {
        EnterMenu();
    }

    [SerializeField]
    private MainMenuUI _mainMenu;

    [SerializeField]
    private HUDUI _hud;
}
