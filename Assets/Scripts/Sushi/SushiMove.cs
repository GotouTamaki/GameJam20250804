using UnityEngine;

public class SushiMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10)] public float SushiMoveSpeed = 3f; //移動速度(Inspector上で変更可)
    [SerializeField] public Vector3 MoveDirection = Vector3.left; //寿司の進行方向(デフォルトは右から左へ)

    // Update is called once per frame
    void Update()
    {
        transform.Translate(MoveDirection.normalized * SushiMoveSpeed *  Time.deltaTime); // 指定方向へ正規化された方向ベクトルで移動
    }
}
