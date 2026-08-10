using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GodControler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, Header("祈りMax")]
    float _prayMax;
    [SerializeField, Header("Unityevent")]
    UnityEvent[] _events;
    [SerializeField, Header("アイテム画像")]
    Sprite[] _itemImage;
    [SerializeField, Header("ゲージアイテム(親)")]
    Image[] _itemGage;
    [SerializeField, Header("アイテムボックス(親)")]
    Image _itemBox;

    public float _pray;
    public bool _isPray;
    public bool _isMax;
    public bool[] _itemDel;
    PrayUIManager _uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiManager = FindFirstObjectByType<PrayUIManager>();
        InItemAll();
        AllItemSetBool(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pray.ToString("0.00"));
        if (_isPray)
        {
            _pray += Time.deltaTime;
            _uiManager.GageControl(_pray, _prayMax);
            if (_pray > 0 && _itemDel[0])
            {

                PrayAction(0);
            }
            else if (_pray >= 5 && _itemDel[1])
            {

                PrayAction(1);
            }
            else if (_pray >= 10 && _itemDel[2])
            {

                PrayAction(2);
            }
            else if (_pray >= 15 && _itemDel[3])
            {

                PrayAction(3);
                _isMax = true;
            }
        }
        else
        {
            if( !_isMax)
            {
                _pray = Mathf.Max(0f, _pray - Time.deltaTime);
                _uiManager.GageControl(_pray, _prayMax);
            }
            
        }

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPray = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        _isPray = false;
    }

    public void PrayAction(int num)
    {
        _events[num].Invoke();
        _itemDel[num] = false;
        if (num == 0)
        {
            Debug.Log("はじめ");
            
        }
        else if (num == 1)
        {
            Debug.Log("薬草");
            _itemBox.sprite = _itemImage[0];
            GetHarb();
        }
        else if (num == 2)
        {
            Debug.Log("バフ");
            _itemBox.sprite = _itemImage[1];
            GetBuff();
        }
        else if (num == 3)
        {
            Debug.Log("熟成薬草");
            _itemBox.sprite = _itemImage[2];
            GetHighHarb();
        }

    }

    public void AllItemSetBool(bool Item)
    {
        _itemDel[0] = Item;
        _itemDel[1] = Item;
        _itemDel[2] = Item;
        _itemDel[3] = Item;
    }
    public void GetHarb()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.AddHarb(1);
    }
    public void GetHighHarb()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.AddHighHarb(1);
    }
    public void GetBuff()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.ChangeState(GameManager.PlayerState.Power);
    }

    public void InItemImage(int num)
    {
        _itemGage[num].sprite = _itemImage[num];
       
    }
    public void InItemAll()
    {
        InItemImage(0);
        InItemImage(1);
        InItemImage(2);
    }
}
