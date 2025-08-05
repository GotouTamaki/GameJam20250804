using UnityEngine;

public enum MoveDirectionType
{
    Left,
    Right,
    Stop,
}

public class SushiMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] public float SushiMoveSpeed = 3f;
    [SerializeField] private MoveDirectionType moveDirection = MoveDirectionType.Left;

    private Vector3 moveVector = Vector3.zero;

    void Start()
    {
        SetDirection(moveDirection); // ← ここで右に設定すれば右に動く
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetDirection(MoveDirectionType.Left);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetDirection(MoveDirectionType.Right);
        }


        transform.Translate(moveVector * SushiMoveSpeed * Time.deltaTime);
    }

    public void SetDirection(MoveDirectionType direction)
    {
        switch (direction)
        {
            case MoveDirectionType.Left:
                moveVector = Vector3.left;
                Debug.Log("Direction set to LEFT");
                break;
            case MoveDirectionType.Right:
                moveVector = Vector3.right;
                Debug.Log("Direction set to RIGHT");
                break;
        }
    }
}
