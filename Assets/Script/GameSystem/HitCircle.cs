using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitCircle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _maxTimer = 2;

    private float _timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(TimerOver(_maxTimer))
        {
            Destroy(gameObject);
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TimerOver(1.5f))
        {
            Debug.Log("nice");
            Destroy(gameObject);
        }
        else if (TimerOver(0.75f))
        {

            Debug.Log("oh");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No");
            Destroy(gameObject);
        }
    }

    public bool TimerOver(float time)
    {
        return _timer > time;
    }
}
