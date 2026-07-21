using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitCircle : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    private float _maxTimer = 2;

    private float _timer;
    private EnemyHelth _enemyHelth;
    private Player _player;
    private DirectionAttack _directionAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _directionAttack = FindFirstObjectByType<DirectionAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(TimerOver(_maxTimer))
        {
            _player.PlayerModifyHelth();
            _player.ModifyStamina();
            Destroy(gameObject);
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_player._stagging)
        {
            Vector3 clickPosition = eventData.pointerPressRaycast.worldPosition;
            if (TimerOver(1.5f))
            {
                _enemyHelth.PlayerDamage();
                _enemyHelth.PlayerStamina(2);
                _directionAttack.Hibana(clickPosition);
                Destroy(gameObject);
            }
            else if (TimerOver(1f))
            {
                _enemyHelth.PlayerStamina();
                _player.ModifyStamina();
                Destroy(gameObject);
            }
            else
            {
                _player.ModifyStamina();
                Destroy(gameObject);
            }
        }
        
    }

    public bool TimerOver(float time)
    {
        return _timer > time;
    }
}
