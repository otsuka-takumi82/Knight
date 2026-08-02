using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionAttack : MonoBehaviour, IPointerDownHandler
{
    private EnemyHelth _enemyHelth;
    private Player _player;
    [SerializeField, UnitHeaderInspectable("火花")]
    private GameObject _hibana;
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
        if(!_player._stagging && _player._canAttack)
        {
            Vector3 clickPosition = eventData.pointerPressRaycast.worldPosition;

            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            Hibana(clickPosition);
            StartCoroutine(_player.AttackCoolTime());
        }
           
          
    }

    public void Hibana(Vector3 hibanapos)
    {
        Instantiate(_hibana, hibanapos, Quaternion.identity);
    }

}
