using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SushiTouch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _clickSprite;
    [SerializeField] private float _destroyDelayTime = 0.5f;

    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ﾐ゜ｯ");
        SoundManager.Instance.PlayShootSFX();

        if (_clickSprite is not null)
        {
            _spriteRenderer.sprite = _clickSprite;
        }

        Destroy(gameObject, 0.5f);
    }
}
