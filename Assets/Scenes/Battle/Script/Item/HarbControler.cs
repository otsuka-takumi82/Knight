using UnityEngine;

public class HarbControler : ItemBase
{
    [SerializeField]
    public float _harbHelth;
    [SerializeField]
    public string _harbName;

    private int _harbNum;
    public override void Awake()
    {
        

       _harbNum =  FindFirstObjectByType<GameManager>()._harb;
        

    }
    public override void Activate()
    {
        FindFirstObjectByType<Player>().AddHelth(_harbHelth);
        _harbNum--;
        _gameManager._harb = _harbNum;
    }

    public override int ItemNum()
    {
        return _harbNum;
    }

    public override string ItemStr()
    {
        return _harbName;
    }

}
