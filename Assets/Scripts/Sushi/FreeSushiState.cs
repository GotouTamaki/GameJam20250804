using System.Collections.Generic;

public class FreeSushiState
{
    private HashSet<SushiType> _freeSushiTypes = new HashSet<SushiType>();
    private float _duration = 0f;
    private float _elapsed = 0f;
    private bool _isActive = false;

    public bool IsActive => _isActive;
    public float RemainingTime => _isActive ? (_duration - _elapsed) : 0f;

    public void Activate(HashSet<SushiType> sushiTypes, float duration)
    {
        _freeSushiTypes.Clear();
        foreach (var type in sushiTypes)
        {
            _freeSushiTypes.Add(type);
        }
        _duration = duration;
        _elapsed = 0f;
        _isActive = true;
    }

    public bool IsFreeSushi(SushiType type)
    {
        return _isActive && _freeSushiTypes.Contains(type);
    }

    public void Update(float deltaTime)
    {
        if (!_isActive) return;

        _elapsed += deltaTime;
        if (_elapsed >= _duration)
        {
            _isActive = false;
            _freeSushiTypes.Clear();
        }
    }

    public void Deactivate()
    {
        _isActive = false;
        _freeSushiTypes.Clear();
    }
}
