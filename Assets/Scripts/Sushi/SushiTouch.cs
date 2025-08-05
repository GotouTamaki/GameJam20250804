using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SushiTouch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _clickSprite;
    [SerializeField] private SushiParameterData _data;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private float _destroyDelayTime = 0.5f;

    private SpriteRenderer _spriteRenderer;
    SushiParameter _sushiParameter;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sushiParameter = _data.sushiParameter;
        _textMeshPro.text = _sushiParameter.price.ToString();
    }

    public void PriceDown()
    {
        if (_sushiParameter is null)
        {
            _sushiParameter.price = 0;
        }
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
