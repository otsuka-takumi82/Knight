
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
    
    
    private bool[] _fastAttack = new bool[4];
    bool _coolOk;
    
    
    public enum AttackType
    {
        RightUp,
        LeftUp,
        RightDown,
        LeftDown

    }
    [SerializeField]
    public AttackType _attackType;
    BattleUIManager _bUI;
    [SerializeField, Header("次のコンボ攻撃")]
    Animator _anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
        _bUI = FindFirstObjectByType<BattleUIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cousle"))
        {
            _bUI.ChangeCursleDirection(_attackType);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_player._stagging)
        {
            if (_player._playerAttackType == _attackType)
            {
                _player._commboNum++;
                _player._currentCoolTime = 0.25f;
                _coolOk = true;

            }
            else
            {
                if(_player._commboNum > _player._saveCommbo)
                {
                    _player._saveCommbo = _player._commboNum;
                }
                //_player._commboNum = 0;
                _player._noCombo = true;
                _player._currentCoolTime = _player._attackCoolTime;
                _coolOk = false;
            }
        }
        
        if (!_player._stagging && _player._canAttack)
        {
            if(_coolOk)
            {
                _anim.SetTrigger("Combo");
            }
            Vector3 clickPosition = eventData.pointerPressRaycast.worldPosition;
            

            //_enemyHelth.PlayerDamage();
            //_enemyHelth.PlayerStamina();
            if (_attackType == AttackType.RightUp)
            { 
                AttackMove(new Vector3(clickPosition.x, clickPosition.y - 1.5f, clickPosition.z));
                _player._playerAttackType = AttackType.LeftDown;
            }
            if (_attackType == AttackType.RightDown)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 1f, clickPosition.z));
                _player._playerAttackType = AttackType.RightUp;
            }
            if (_attackType == AttackType.LeftUp)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 0, clickPosition.z));
                _player._playerAttackType = AttackType.RightDown;
            }
            if (_attackType == AttackType.LeftDown)
            {
                AttackMove(new Vector3(clickPosition.x, clickPosition.y + 0, clickPosition.z));
                _player._playerAttackType = AttackType.LeftUp;
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

