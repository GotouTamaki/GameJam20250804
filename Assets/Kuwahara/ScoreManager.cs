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
    [SerializeField] private Image _stomachFillImage = default;

    private int _stomachFill = default;

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
        _stomachFillImage.fillAmount = (float)StomachFill / _maxStomachFill; // Assuming max stomach fill is 100
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



