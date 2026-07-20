using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionAttack : MonoBehaviour, IPointerDownHandler
{
    private EnemyHelth _enemyHelth;
    private Player _player;
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
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            StartCoroutine(_player.AttackCoolTime());
        }
           
          
    }
}
