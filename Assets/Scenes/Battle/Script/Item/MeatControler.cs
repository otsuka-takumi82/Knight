using UnityEngine;

public class MeatCotroler : ItemBase
{
    [SerializeField]
    public float _meatStamina;
    [SerializeField]
    public string _meatName;

    private int _meatNum;
    public override void Awake()
    {
        _meatNum = FindFirstObjectByType<GameManager>()._meat;
    }
    public override void Activate()
    {
        FindFirstObjectByType<Player>().AddStamina(_meatStamina);
        _meatNum--;
        FindFirstObjectByType<GameManager>()._meat = _meatNum;
    }

    public override int ItemNum()
    {
        return _meatNum;
    }

    public override string ItemStr()
    {
        return _meatName;
    }
}
