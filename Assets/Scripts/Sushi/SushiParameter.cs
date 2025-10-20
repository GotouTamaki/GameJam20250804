using UnityEngine;

[System.Serializable]
public class SushiParameter
{
    [SerializeField] private SushiType type = SushiType.Squid;
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _clickSprite;
    [SerializeField] private int addScore = 10;
    [SerializeField] private int price = 100;
    [SerializeField] private int fillStomach = 10;

    public SushiType Type => type;

    public Sprite IdleSprite => _idleSprite;

    public Sprite ClickSprite => _clickSprite;

    public int AddScore => addScore;

    public int Price => price;

    public void SetPrice(int price) => this.price = price;

    public int FillStomach => fillStomach;
}
