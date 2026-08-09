using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GodControler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, Header("祈りMax")]
    float _prayMax;
    [SerializeField, Header("Unityevent")]
    UnityEvent[] _events;

    public float _pray;
    public bool _isPray;
    public bool[] _itemDel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AllItemSetBool(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pray.ToString("0.00"));
        if (_isPray)
        {
            _pray += Time.deltaTime;
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
            }
        }
        else
        {

            _pray = Mathf.Max(0f,_pray - Time.deltaTime);
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
            GetHarb();
        }
        else if (num == 2)
        {
            Debug.Log("バフ");
            GetBuff();
        }
        else if (num == 3)
        {
            Debug.Log("熟成薬草");
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
}
