using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Action OnNewGame;
    public Action OnExit;

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
        OnNewGame = _mainMenu.OnNewGame;
        OnExit = _mainMenu.OnExit;

        _mainMenu.SetActive(true);
        _hud.SetActive(false);
    }

    [SerializeField]
    private MainMenuUI _mainMenu;

    [SerializeField]
    private HUDUI _hud;
}
