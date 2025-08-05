using UnityEngine;
using System.Collections.Generic;

public class SushiSpawn : MonoBehaviour
{
    [SerializeField] GameObject sushiPrefab;

    // 指定方向で寿司を生成＆動かす
    public void SpawnSushi(MoveDirection direction)
    {
        GameObject sushiObj = Instantiate(sushiPrefab, transform.position, Quaternion.identity);
        SushiMove move = sushiObj.GetComponent<SushiMove>();
        if (move == null)
        {
            Debug.LogError("SushiMoveが付いていません！");
            return;
        }
        move.SetDirection(direction);
    }
}
