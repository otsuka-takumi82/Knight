
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<Wepon> _wepon;
    public List<int> _stageNum;
    public List<GameManager.Item> _item;
    public int _currentDayNum;
    public int _noPrayDay;
    public int _currentEquipped;
    public int _prayLevel;
    public int _money;
    public bool[] _killEnemy;
    public bool _isArmored;
    public float[] _prayPile;
    public int _harb;
    public int _highHarb;
    public int _meat;

}
