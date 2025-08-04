using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SushiMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10)] public float SushiMoveSpeed = 3f;
    [SerializeField] public Vector3 MoveDirection = Vector3.left;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(MoveDirection.normalized * SushiMoveSpeed *  Time.deltaTime);
    }
}
