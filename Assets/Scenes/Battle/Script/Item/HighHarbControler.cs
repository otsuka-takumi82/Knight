using UnityEngine;

public class HighHarbControler : ItemBase
{
    [SerializeField]
    public float _highHarbHelth;
    [SerializeField]
    public string _highHarbName;

    private int _highHarbNum;
    public override void Awake()
    {
        _highHarbNum = FindFirstObjectByType<GameManager>()._highHarb;
    }
    public override void Activate()
    {
        FindFirstObjectByType<Player>().AddHelth(_highHarbHelth);
        _highHarbNum--;
        FindFirstObjectByType<GameManager>()._highHarb = _highHarbNum;
    }

    public override int ItemNum()
    {
        return _highHarbNum;
    }

    public override string ItemStr()
    {
        return _highHarbName;
    }
}
