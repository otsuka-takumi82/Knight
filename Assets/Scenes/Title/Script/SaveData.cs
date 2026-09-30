
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<Wepon> _wepon = new List<Wepon>()
    {
        new Wepon
        {
            _name = "初期武器",
            _weponState = WeponEnum.Sword,
            _weponPower = -1,
            _isCrafted = true,
            _repairPal = 2
        },
        new Wepon
        {
            _name = "剣",
            _weponState = WeponEnum.Sword,
            _weponPower = -3,
            _isCrafted = false,
            _repairPal = 0
        },
        new Wepon
        {
            _name = "メイス",
            _weponState = WeponEnum.Mace,
            _weponPower = -5,
            _isCrafted = false,
            _repairPal = 0
        }
    };

    public List<int> _stageNum = new List<int>(){1,1,3,2,1};
    public List<GameManager.Item> _item = new List<GameManager.Item>()
    {
        GameManager.Item.None,
        GameManager.Item.None,
        GameManager.Item.None,
        GameManager.Item.None
    };
    public int _currentTimeNum = 0;
    public int _currentDayNum = 0;
    public int _noPrayDay = 0;
    public int _currentEquipped = 0;
    public int _prayLevel = 0;
    public int _money = 0;
    public bool[] _killEnemy = new bool[6] {false,false,false,false,false,false};
    public bool _isArmored = false;
    public float[] _prayPile = new float[] { 1.0f, 1.1f };
    public int _harb = 0;
    public int _highHarb = 0;
    public int _meat = 5;

}
