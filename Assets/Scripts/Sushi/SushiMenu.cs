using System.Collections.Generic;
using UnityEngine;

public class SushiMenu : MonoBehaviour
{
    [SerializeField] private int _selectfreeSushiTypeCount = 4;
    [SerializeField] private float _freeSushiDuration = 10f;

    private FreeSushiState _freeSushiState;
    List<SushiType> _allSushis = new List<SushiType>();

    public FreeSushiState FreeSushiState => _freeSushiState;

    void Start()
    {
        _allSushis = new List<SushiType>((SushiType[])System.Enum.GetValues(typeof(SushiType)));
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
        HashSet<SushiType> selected = new HashSet<SushiType>();

        while (selected.Count < _selectfreeSushiTypeCount)
        {
            int randomIndex = Random.Range(0, _allSushis.Count);
            selected.Add(_allSushis[randomIndex]);
        }

#if UNITY_EDITOR
        foreach (SushiType s in selected)
        {
            switch (s)
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

        _freeSushiState.Activate(selected, _freeSushiDuration);
    }

    private void OnDestroy()
    {
        _freeSushiState?.Deactivate();
    }
}
