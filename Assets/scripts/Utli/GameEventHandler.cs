using System;
using UnityEngine;

public static class GameEventHandler
{
    public static Action<int> OnAstroDestroy;
    public static Action OnForceAstroDestroy;
    public static Action<float> OnPlayerTakeDamage;
    public static Action<int> OnScoreChanged;
    public static Action OnGameOver;
    public static Action OnEnterPlayScene;
    public static Action OnEnterMainMenu;
    public static Action OnGamePuased;
    public static void CutConnections()
    {
        OnAstroDestroy = null;
        OnForceAstroDestroy = null;
        OnPlayerTakeDamage = null;
        OnScoreChanged = null;
        OnGameOver = null;
        OnEnterPlayScene = null;
        OnEnterMainMenu = null;
        OnGamePuased = null;
    }
}
