using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;   // List を使うために必要
using System.Linq;
using System;

public class ItemBase : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    public int _itemUINum;
    [SerializeField]
    GameManager.Item _itemType;

    public GameManager _gameManager;
    public virtual void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        int num = _gameManager._item.FindIndex(1, 3, x => x == _itemType);
        _itemUINum = num;
        //for (int i = 1; i < 4; i++)
        //{
        //    if (_gameManager._item[i] == _itemType)
        //    {
        //        _itemUINum = i;
        //    }
        //}
        FindFirstObjectByType<BattleUIManager>().ChangeItemText(_itemUINum, ItemStr(), ItemNum());
        FindFirstObjectByType<BattleUIManager>().ChangePorch(ItemStr(), _itemUINum);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ItemBool(ItemNum()))
        {
            Activate();
            FindFirstObjectByType<BattleUIManager>().ChangeItemText(_itemUINum,ItemStr(), ItemNum());
            Debug.Log("共通機能");
        }
        
    }

    public virtual void Activate()
    {

    }

    public bool ItemBool(float num)
    {
        return num > 0;
    }

    public virtual int ItemNum()
    {
        return 1;
    }
    public virtual string ItemStr()
    {
        return "a";
    }

}
