using UnityEngine;

public class SushiManager : MonoBehaviour
{
    [SerializeField] SushiMove sushiMove; // 対象のSushiMoveコンポーネントをInspectorでセット

    public void MoveLeft()
    {
        sushiMove.SetDirection(MoveDirection.Left);
    }

    public void MoveRight()
    {
        sushiMove.SetDirection(MoveDirection.Right);
    }
}
