using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum MoveDirection
{
    Left,Right
}
public class SushiMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] public float SushiMoveSpeed = 3f;
    private Vector3 moveVector = Vector3.zero;

    void Start()
    {
        SetDirection(MoveDirection.Right); // ← ここで右に設定すれば右に動く
    }

    void Update()
    {
        transform.Translate(moveVector * SushiMoveSpeed * Time.deltaTime);
    }

    public void SetDirection(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                moveVector = Vector3.left;
                Debug.Log("Direction set to LEFT");
                break;
            case MoveDirection.Right:
                moveVector = Vector3.right;
                Debug.Log("Direction set to RIGHT");
                break;
        }
    }
}
