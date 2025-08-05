using UnityEngine;
using UnityEngine.Events;

public class ScoreTester : MonoBehaviour
{
    [SerializeField] UnityEvent _scoreTest;
    [SerializeField] UnityEvent _moneyTest;

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
