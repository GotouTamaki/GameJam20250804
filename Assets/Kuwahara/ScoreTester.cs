using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ScoreTester : MonoBehaviour
{
    [SerializeField] UnityEvent _scoreTest;
    [SerializeField] UnityEvent _moneyTest; 

    public void ScoreUp(int i)
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.Score += i; // スコアを100増やす
        }
    }

    public void MoneyDown(int i)
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.Money += i; // マネーを50減らす
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _scoreTest?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            _moneyTest?.Invoke();
        }
    }
}
