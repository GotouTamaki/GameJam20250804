using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SushiTouch : MonoBehaviour
{
    [SerializeField] private SushiParameterData _data;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private float _destroyDelayTime = 0.5f;
    [SerializeField] private Color _freeSushiTextColor = Color.yellow;

    private SpriteRenderer _spriteRenderer;
    private SushiParameter _sushiParameter;
    private ScoreManager _scoreManager;
    private SushiManager _sushiManager;
    private SushiMenu _sushiMenu;
    private SushiMove _sushiMove;
    // 自分が格納されているプール
    private ObjectPool<SushiTouch> _pool;
    private FreeSushiState _freeSushiState;

    //private bool _isEnter = false;
    private bool _isClick = false;

    //public SushiParameter SushiParameter => _sushiParameter;
    public SushiParameterData SushiParameterData => _data;
    public SushiMove SushiMove => _sushiMove;

    public void SetPool(ObjectPool<SushiTouch> pool)
    {
        _pool = pool;
    }

    public void SetFreeSushiState(FreeSushiState state)
    {
        _freeSushiState = state;
    }

    private void Awake()
    {
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        _sushiManager = FindAnyObjectByType<SushiManager>();
        _sushiMenu = FindAnyObjectByType<SushiMenu>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _sushiParameter = _data.sushiParameter;
        _freeSushiState = _sushiMenu.FreeSushiState;
        _spriteRenderer.sprite = _data.sushiParameter.IdleSprite;
        _isClick = false;

        _sushiMove = GetComponent<SushiMove>();
        if (_sushiMove == null)
        {
            _sushiMove = GetComponentInChildren<SushiMove>();
        }

        UpdatePriceDisplay();
    }

    private void Update()
    {
        // 無料寿司の状態が変わったら表示を更新
        if (_freeSushiState != null)
        {
            UpdatePriceDisplay();
        }
    }

    private void UpdatePriceDisplay()
    {
        if (_textMeshPro == null || _sushiParameter == null || _freeSushiState == null) return;

        int displayPrice = GetCurrentPrice();
        _textMeshPro.text = IntToKanjiString(displayPrice);

        if (_freeSushiState.IsFreeSushi(_sushiParameter.Type))
        {
            _textMeshPro.color = _freeSushiTextColor;
        }
        else
        {
            _textMeshPro.color = Color.black;
        }
    }

    private int GetCurrentPrice()
    {
        if (_freeSushiState != null && _freeSushiState.IsFreeSushi(_sushiParameter.Type))
        {
            return 0;
        }
        return _sushiParameter.Price;
    }

    #region 漢字変換
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
    #endregion

    public void OnMouseDown()
    {
        if (UIManager.Instance.IsPlayGame && !_isClick)
        {
            SoundManager.Instance.PlayShootSFX();
            _scoreManager.AddScore(_sushiParameter.AddScore);
            _scoreManager.AddMoney(-GetCurrentPrice());
            _scoreManager.AddStomachFill(_sushiParameter.FillStomach);
            _sushiMove.SetDirection(MoveDirectionType.Stop);

            if (_data.sushiParameter.ClickSprite is not null)
            {
                _spriteRenderer.sprite = _data.sushiParameter.ClickSprite;
            }

            ReturnToPoolWithDelay().Forget();
            _isClick = true;
        }
    }

    private async UniTaskVoid ReturnToPoolWithDelay()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(_destroyDelayTime));

        // プールに返却
        _pool?.Release(this);
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
