using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    public int _itemUINum;
    public virtual void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
            FindFirstObjectByType<BattleUIManager>().ChangeItemText(_itemUINum,ItemStr(), ItemNum());
            FindFirstObjectByType<BattleUIManager>().ChangePorch(ItemStr(),_itemUINum);

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
