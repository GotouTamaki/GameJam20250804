using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField, Header("初期スコア")] private int _initScore = default;
    [SerializeField, Header("初期所持金")] private int _initMoney = default;
    [SerializeField, Header("最大満腹値")] private float _maxStomachFill = 100f;

    [Header("オブジェクトアタッチ用")]
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    [SerializeField] private TextMeshProUGUI _moneyText = default;
    [SerializeField] private Slider _stomachFillSlider = default;
    [SerializeField] private Image _girlImage = default;
    [SerializeField] private Sprite _girlImageFull;
    [SerializeField] private Sprite _girlImageHungry;
    [SerializeField] private Sprite _girlImageNormal;
    [SerializeField] private Sprite _girlImagePerfentFull;


    [SerializeField, Range(0, 150)]private int _stomachFill = default;
    private float _hungerTimer = 0f;

    private void Start()
    {
        _stomachFillSlider.maxValue = (int)_maxStomachFill; // 満腹値を最大値に設定
        // UIの初期化
        UpdateScore();
        UpdateMoney();
        UpdateStomachFill();
    }

    public int Score
    {
        get { return _initScore; }
        set
        {
            _initScore = value;
            Debug.Log("Score updated: " + _initScore);
            UpdateScore();
        }
    }

    public int Money
    {
        get { return _initMoney; }
        set
        {
            _initMoney = value;
            Debug.Log("Money updated: " + _initMoney);
            UpdateMoney();
        }
    }

    public int StomachFill
    {
        get { return _stomachFill; }
        set
        {
            _stomachFill = value;
            Debug.Log("StomachFill updated: " + _stomachFill);
            UpdateStomachFill();
        }
    }

    public void AddScore(int addValue)
    {
        Score += addValue; // スコアを100増やす
    }

    public void AddMoney(int addValue)
    {
        Money += addValue; // マネーを50減らす
    }

    public void AddStomachFill(int addValue)
    {
        StomachFill += addValue; // 満腹値を数値分増やす
        if (StomachFill > _maxStomachFill)
        {
            StomachFill = (int)_maxStomachFill; // 満腹値の上限を設定
        }
    }

    private void Update()
    {
        _hungerTimer += Time.deltaTime;
        if (_hungerTimer >= 1f) // 1秒ごとに満腹値を減らす
        {
            _hungerTimer = 0f;
            StomachFill -= 1; // 満腹値を1減らす
            if (StomachFill < 0)
            {
                StomachFill = 0; // 満腹値の下限を設定
            }
        }
    }

    void UpdateScore()
    {
        _scoreText.text = IntToKanjiString(Score);
    }

    void UpdateMoney()
    {
        _moneyText.text = IntToKanjiString(Money);
    }

    void UpdateStomachFill()
    {
        if (_stomachFill > _maxStomachFill)
        {
            _stomachFill = (int)_maxStomachFill; // 満腹値の上限を設定
        }
        _stomachFillSlider.value = StomachFill; // Assuming max stomach fill is 100
        if (StomachFill >= _maxStomachFill)
        {
            _girlImage.sprite = _girlImagePerfentFull; // 完全満腹状態の画像
        }
        else if (StomachFill <= 20)
        {
            _girlImage.sprite = _girlImageHungry; // 空腹状態の画像
        }
        else if (StomachFill >= 80)
        {
            _girlImage.sprite = _girlImageFull; // 満腹状態の画像
        }
        else
        {
            _girlImage.sprite = _girlImageNormal; // 通常状態の画像
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

}



