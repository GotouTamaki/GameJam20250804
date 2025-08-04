using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("初期所持金")]
    private int _score = default;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Score updated: " + _score);
            UpdateScore();
        }
    }

    [SerializeField] int _money = default;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            Debug.Log("Money updated: " + _money);
            UpdateMoney();
        }
    }

    [Header("オブジェクトアタッチ用")]
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    [SerializeField] private TextMeshProUGUI _moneyText = default;

    void UpdateScore()
    {
        _scoreText.text = IntToKanjiString(Score);
    }
    void UpdateMoney()
    {
        _moneyText.text = IntToKanjiString(Money);
    }

    public string IntToKanjiString(int value)
    {
        string returnString = "";
        char[] _exchangeChar = value.ToString().ToCharArray();
        for(int i = 0; i < _exchangeChar.Length; i++)
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
            
        
        
