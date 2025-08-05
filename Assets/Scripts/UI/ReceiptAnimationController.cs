using System.Collections;
using UnityEngine;

public class ReceiptAnimationController : MonoBehaviour
{
    [Header("Animation Details")]
    public RectTransform rectMover;       
    public Vector2 targetAnchoredPos;
    public float slideDuration = 1f;

    private Vector2 startAnchoredPos;
    private Coroutine slideRoutine;

    private void Start()
    {
        startAnchoredPos = targetAnchoredPos + new Vector2(0, 305f + rectMover.rect.height);

        rectMover.anchoredPosition = startAnchoredPos;
    }

    private IEnumerator SlideToTarget()
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);
            float easedT = 1 - Mathf.Pow(1 - t, 3); // Ease-out cubic

            rectMover.anchoredPosition = Vector2.Lerp(startAnchoredPos, targetAnchoredPos, easedT);
            yield return null;
        }

        rectMover.anchoredPosition = targetAnchoredPos;
    }
}
