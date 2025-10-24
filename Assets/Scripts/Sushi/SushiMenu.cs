using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SushiMenu : MonoBehaviour
{
    [SerializeField] private SushiTouch[] _allSushis;
    [SerializeField] private Image[] _menuImages;
    [SerializeField] private int _selectfreeSushiTypeCount = 4;
    [SerializeField] private float _freeSushiDuration = 10f;

    private FreeSushiState _freeSushiState;

    public SushiTouch[] AllSushuis => _allSushis;

    public FreeSushiState FreeSushiState => _freeSushiState;

    void Start()
    {
        //_allSushisList = new List<SushiType>((SushiType[])System.Enum.GetValues(typeof(SushiType)));
        _freeSushiState = new FreeSushiState();
        SelectSushiType();
    }

    private void Update()
    {
        if (_freeSushiState.IsActive is false)
        {
            SelectSushiType();
        }

        _freeSushiState.Update(Time.deltaTime);
    }

    public FreeSushiState GetFreeSushiState()
    {
        return _freeSushiState;
    }

    public void SelectSushiType()
    {
        HashSet<SushiTouch> selected = new HashSet<SushiTouch>();

        // 重複なしの選択方法はもっといい方法があるだろうがが一旦これで
        while (selected.Count < _selectfreeSushiTypeCount)
        {
            int randomIndex = Random.Range(0, _allSushis.Length);
            selected.Add(_allSushis[randomIndex]);
        }



#if UNITY_EDITOR
        foreach (SushiTouch s in selected)
        {
            switch (s.SushiParameterData.sushiParameter.Type)
            {
                case SushiType.Bonito:
                    Debug.Log("Bonitoが選ばれた");
                    break;
                case SushiType.Kappamaki:
                    Debug.Log("Kappamakiが選ばれた");
                    break;
                case SushiType.Negitoro:
                    Debug.Log("Negitoroが選ばれた");
                    break;
                case SushiType.Octopus:
                    Debug.Log("Octopusが選ばれた");
                    break;
                case SushiType.Omelet:
                    Debug.Log("Omeletが選ばれた");
                    break;
                case SushiType.Parfait:
                    Debug.Log("Parfaitが選ばれた");
                    break;
                case SushiType.SalmonRoe:
                    Debug.Log("SalmonRoeが選ばれた");
                    break;
                case SushiType.Shrimp:
                    Debug.Log("Shrimpが選ばれた");
                    break;
                case SushiType.Squid:
                    Debug.Log("Squidが選ばれた");
                    break;
                case SushiType.Tuna:
                    Debug.Log("Tunaが選ばれた");
                    break;
            }
        }
#endif

        int index = 0;

        foreach (SushiTouch s in selected)
        {
            _menuImages[index].sprite = s.SushiParameterData.sushiParameter.MenuSprite;
            index++;
        }

        _freeSushiState.Activate(selected, _freeSushiDuration);
    }

    private void OnDestroy()
    {
        _freeSushiState?.Deactivate();
    }
}
