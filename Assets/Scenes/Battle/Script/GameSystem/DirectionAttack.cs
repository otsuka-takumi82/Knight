
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DirectionAttack : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, Header("右上攻撃")]
    UnityEvent[] _events;
    private EnemyHelth _enemyHelth;
    private Player _player;
    [SerializeField, Header("右上攻撃")]
    private GameObject _arm;
    
    
    private enum AttackType
    {
        RightUp,
        LeftUp,
        RightDown,
        LeftDown

    }
    [SerializeField]
    private AttackType _attackType;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_player._stagging && _player._canAttack)
        {
            Vector3 clickPosition = eventData.pointerPressRaycast.worldPosition;

            //_enemyHelth.PlayerDamage();
            //_enemyHelth.PlayerStamina();
            if (_attackType == AttackType.RightUp)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y - 1.5f, clickPosition.z));
            }
            if (_attackType == AttackType.RightDown)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 1f, clickPosition.z));
            }
            if (_attackType == AttackType.LeftUp)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 0, clickPosition.z));
            }
            if (_attackType == AttackType.LeftDown)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 0, clickPosition.z));
            }
            StartCoroutine(_player.AttackCoolTime());
        }


    }

    public void AttackMove(Vector3 pos)
    {
        _arm.transform.position = pos;
        _events[0].Invoke();
    }
   
    
}

