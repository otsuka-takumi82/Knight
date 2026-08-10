using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;
#region 武器構造体
public enum WeponEnum
{
    DefautSword,
    Sword,
    Mace
}
[Serializable]
public struct Wepon
{
    public string _name;
    public WeponEnum _weponState;
    public float _weponPower;
    public bool _isCrafted;
    public int _repairPal;
}
#endregion
public class GameManager : MonoBehaviour
{
    public List<Wepon> _wepon;
    public enum PlayerState
    { 
        Nomal,
        Power,
        Guald
    }
    public enum Item
    {
        None,
        Harb,
        HighHarb,
        Meat
    }
    [SerializeField, Header("プレイヤー状態")]
    public PlayerState _playerState;
    /// <summary>
    /// １～のステージ番号。
    /// １には祈り、２には戦闘、３には鍛冶が入ってる
    /// </summary>
    [SerializeField]
    public List<int> _stageNum;
    [SerializeField]
    public Item[] _item;

    /// <summary>
    /// １～の時間指定変数
    /// </summary>
    public int _currentTimeNum;
    public int _currentEquipped = 0;
    public int _harb;
    public int _highHarb;
    public int _meat = 5;
    public Vector3 _defaultTransform;
    private void Awake()
    {
        if (FindObjectsByType<GameManager>(
    FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_wepon[_currentEquipped]._repairPal);
    }

    #region アイテム追加。多態性アイテム作るときにここも
    public void AddItem(int addItem, int num)
    {
        addItem += num;
    }
    public void AddHarb(int num)
    {
        _harb += num;
    }
    public void AddHighHarb(int num)
    {
        _highHarb += num;
    }
    #endregion
    public Wepon CurrentWepon
    {
        get
        {
            return _wepon[_currentEquipped];
        }
    }
    public void AddRepair(int num)
    {
        Wepon wepon;
        wepon = _wepon[_currentEquipped];
        wepon._repairPal =  Mathf.Clamp(wepon._repairPal + num,0,2);
        _wepon[_currentEquipped] = wepon;
    }
    public void ChangeState(PlayerState buff)
    {
        _playerState = buff;
    }
    /// <summary>
    /// アイテムポーチ変更メソッド
    /// </summary>
    /// <param name="item">変更するアイテムの種類</param>
    /// <param name="num">アイテムポーチの番号（１から）</param>
    public void ChangeItem(Item item, int num)
    {
        _item[num] = item;
    }
    public bool State(PlayerState state)
    {
        return _playerState == state;
    }
}
