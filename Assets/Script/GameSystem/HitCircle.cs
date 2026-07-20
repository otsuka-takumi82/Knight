using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitCircle : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    private float _maxTimer = 2;

    private float _timer;
    private EnemyHelth _enemyHelth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TimerOver(1.5f))
        {
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            Destroy(gameObject);
        }
        else if (TimerOver(1f))
        {
            _enemyHelth.PlayerStamina();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool TimerOver(float time)
    {
        return _timer > time;
    }
}
