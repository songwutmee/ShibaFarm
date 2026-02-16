using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MainMenu, Playing, DebtFree, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Status")]
    public GameState currentState = GameState.Playing;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GameEvents.OnMoneyChanged += CheckWinCondition;
    }

    private void OnDestroy()
    {
        GameEvents.OnMoneyChanged -= CheckWinCondition;
    }

    private void CheckWinCondition(int balance)
    {
        if (DebtManager.Instance.remainingDebt <= 0 && currentState != GameState.DebtFree)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        currentState = GameState.DebtFree;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}