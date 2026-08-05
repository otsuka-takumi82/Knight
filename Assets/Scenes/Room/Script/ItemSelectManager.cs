using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class ItemSelectManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField]
    int _porchIndex;

    RectTransform _rectTransform;
    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 当たり判定を透過させて後ろのポーチに届くようにする！
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        if (gameObject != null)
        {
            if (gameObject.CompareTag("Porch1"))
            {
                FindFirstObjectByType<GameManager>().ChangeItem(GameManager.Item.Harb, 0);
            }
            GetComponent<CanvasGroup>().blocksRaycasts = true; // 当たり判定を透過させて後ろのポーチに届くようにする！
        }
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void ItemChange()
    {
        _gameManager.ChangeItem(GameManager.Item.Harb, _porchIndex);
    }


}
