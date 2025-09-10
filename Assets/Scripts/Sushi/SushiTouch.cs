using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SushiTouch : MonoBehaviour
{
    [SerializeField] private Sprite _clickSprite;
    [SerializeField] private SushiParameterData _data;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private float _destroyDelayTime = 0.5f;

    private SpriteRenderer _spriteRenderer;
    private SushiParameter _sushiParameter;

    private ScoreManager _scoreManager;
    private SushiManager _sushiManager;
    private SushiMove _sushiMove;

    private bool _isEnter = false;

    public SushiParameter SushiParameter => _sushiParameter;
    public SushiMove SushiMove => _sushiMove;

    private void OnEnable()
    {
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        _sushiManager = FindAnyObjectByType<SushiManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sushiParameter = _data.sushiParameter;
        _textMeshPro.text = IntToKanjiString(_sushiParameter.Price);
        _sushiMove = FindAnyObjectByType<SushiMove>();
    }

    public void PriceDown()
    {
        if (_sushiParameter is null)
        {
            _sushiParameter.SetPrice(0);
        }
    }

    public string IntToKanjiString(int value)
    {
        string returnString = "";
        char[] _exchangeChar = value.ToString().ToCharArray();

        for (int i = 0; i < _exchangeChar.Length; i++)
        {
            switch (_exchangeChar[i])
            {
                case '0':
                    returnString += "〇";
                    break;
                case '1':
                    returnString += "一";
                    break;
                case '2':
                    returnString += "二";
                    break;
                case '3':
                    returnString += "三";
                    break;
                case '4':
                    returnString += "四";
                    break;
                case '5':
                    returnString += "五";
                    break;
                case '6':
                    returnString += "六";
                    break;
                case '7':
                    returnString += "七";
                    break;
                case '8':
                    returnString += "八";
                    break;
                case '9':
                    returnString += "九";
                    break;
            }

            returnString += "\n";
        }

        return returnString;
    }

    public void OnMouseDown()
    {
        //Debug.Log("ﾐ゜ｯ");
        SoundManager.Instance.PlayShootSFX();
        _scoreManager.AddScore(_sushiParameter.AddScore);
        _scoreManager.AddMoney(-_sushiParameter.Price);
        _scoreManager.AddStomachFill(_sushiParameter.FillStomach);
        _sushiMove.SetDirection(MoveDirectionType.Stop);

        if (_clickSprite is not null)
        {
            _spriteRenderer.sprite = _clickSprite;
        }

        Destroy(gameObject);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    _isEnter = true;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    _isEnter = false;
    //}
}
