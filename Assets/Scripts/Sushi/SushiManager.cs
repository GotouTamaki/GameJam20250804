using UnityEngine;

public class SushiManager : MonoBehaviour
{
    [SerializeField] SushiMove sushiMove; // 対象のSushiMoveコンポーネントをInspectorでセット

    public void MoveLeft()
    {
        sushiMove.SetDirection(MoveDirectionType.Left);
    }

    public void MoveRight()
    {
        sushiMove.SetDirection(MoveDirectionType.Right);
    }
}
