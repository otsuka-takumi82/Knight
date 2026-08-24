using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class ItemSelectManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField]
    int _porchIndex;
    [SerializeField]
    GameManager.Item _itemSel;
    GameObject[] _porch = new GameObject[4];
    Canvas _canvas;
    Transform _originalParent;
    Vector3 _defaultTransform;


    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvas = FindFirstObjectByType<Canvas>();
        _gameManager = FindFirstObjectByType<GameManager>();
        OriginalGet();
        _porch[1] = GameObject.FindGameObjectWithTag("Porch1");
        _porch[2] = GameObject.FindGameObjectWithTag("Porch2");
        _porch[3] = GameObject.FindGameObjectWithTag("Porch3");
        
        for (int i = 1; i <= 3; i++)
        {
            if(_gameManager._item[i] != GameManager.Item.None)
            {
                Debug.Log("a");
                if (_gameManager._item[i] == _itemSel)
                {
                    Debug.Log("b");
                    ElseSet(_porch[i]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.SetParent(_canvas.transform,false);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        OriginalGet();
        Transform parentSlot = transform.parent;
        if (parentSlot != null)
        {
            if (parentSlot.CompareTag("Porch1"))
            {
                FindFirstObjectByType<GameManager>().ChangeItem(GameManager.Item.None, 1);
            }
            if (parentSlot.CompareTag("Porch2"))
            {
                FindFirstObjectByType<GameManager>().ChangeItem(GameManager.Item.None, 2);
            }
            if (parentSlot.CompareTag("Porch3"))
            {
                FindFirstObjectByType<GameManager>().ChangeItem(GameManager.Item.None, 3);
            }
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 当たり判定を透過させて後ろのポーチに届くようにする！
        
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        if (gameObject != null)
        {
            if (gameObject.CompareTag("Porch1"))
            {
                transform.SetParent(gameObject.transform, false);
                transform.localPosition = Vector3.zero;
                FindFirstObjectByType<GameManager>().ChangeItem(_itemSel, 1);
            }
            else if (gameObject.CompareTag("Porch2"))
            {
                transform.SetParent(gameObject.transform, false);
                transform.localPosition = Vector3.zero;
                FindFirstObjectByType<GameManager>().ChangeItem(_itemSel, 2);
            }
            else if (gameObject.CompareTag("Porch3"))
            {
                transform.SetParent(gameObject.transform, false);
                transform.localPosition = Vector3.zero;
                FindFirstObjectByType<GameManager>().ChangeItem(_itemSel, 3);
            }
            else if (gameObject.CompareTag("Strage"))
            {
                transform.SetParent(gameObject.transform, false);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                OriginalSet();
            }
            
        }
        else
        {
            OriginalSet();
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true; // 当たり判定を透過させて後ろのポーチに届くようにする！
        
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void ItemChange()
    {
        _gameManager.ChangeItem(_itemSel, _porchIndex);
    }

    public void OriginalGet()
    {
        _defaultTransform = transform.position;
        _originalParent = transform.parent;
    }
    public void OriginalSet()
    {
        transform.SetParent(_originalParent, false);
        transform.position = _defaultTransform;
    }
    public void ElseSet(GameObject gameObject)
    {
        transform.SetParent(gameObject.transform, false);
        transform.localPosition = Vector3.zero;
    }


}
