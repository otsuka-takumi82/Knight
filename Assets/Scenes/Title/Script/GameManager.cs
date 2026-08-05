using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public enum PlayerState
    { 
        Nomal,
        Powor,
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
    [SerializeField]
    public List<int> _stageNum;
    [SerializeField]
    public Item[] _item;

    public int _currentTimeNum;
    public int _harb;
    public int _highHarb;
    public int _meat = 5;
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
        //Debug.Log(_item[0]);
    }

    public void AddHarb(int num)
    {
        _harb += num;
    }
    public void AddHighHarb(int num)
    {
        _highHarb += num;
    }
    public void ChangeState(PlayerState buff)
    {
        _playerState = buff;
    }
    public void ChangeItem(Item item, int num)
    {
        _item[num] = item;
    }
    public bool State(PlayerState state)
    {
        return _playerState == state;
    }
}
