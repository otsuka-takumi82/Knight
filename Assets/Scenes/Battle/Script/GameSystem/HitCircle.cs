using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitCircle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private float _maxTimer = 2;
    Animator _anim;
    [SerializeField]
    string _name;

    private float _timer;
    private EnemyHelth _enemyHelth;
    private Player _player;
    private DirectionAttack _directionAttack;
    bool _paused;
    
GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;
    HitSponer _hs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _directionAttack = FindFirstObjectByType<DirectionAttack>();
        _anim = GetComponent<Animator>();
        _hs = FindFirstObjectByType<HitSponer>();
        Color color = _spriteRenderer.color;
        if(_name != null)
        {
            _anim.Play(_name);
        }
        color = new Color(0f, 0f, 0f, 0f);
        _spriteRenderer.color = color;
    }
    void OnEnable()
    {
        _gameManager._pauseReseum += PauseResume;
    }
    void OnDisable()
    {
        _gameManager._pauseReseum -= PauseResume;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!_paused)
        {
            _timer += Time.deltaTime;
        }
        if (_player._isDead || _enemyHelth._stagging)
        {
            Destroy(gameObject);
        }
        if (TimerOver(_maxTimer))
        {
            if(_hs._attack == HitSponer.AttackState.Nomal)
            {
                _player.PlayerModifyHelth();
                _player.ModifyStamina();
            }
            else if(_hs._attack == HitSponer.AttackState.Stamina)
            {
                if(_player._stagging)
                {
                    _player.PlayerModifyHelth();
                }
                else
                {
                    _player.ModifyStamina(1.5f);
                }
                _hs._attack = HitSponer.AttackState.Nomal;
            }
            Destroy(gameObject);
        }
            if (TimerOver(_maxTimer * 0.75f))
            {
            _spriteRenderer.color = Color.red;
            TagChange2();
                
            }
            else if (TimerOver(_maxTimer * 0.5f) && _timer < _maxTimer * 0.75f)
            {
                _collider.enabled = true;
                TagChange1();
               
            _spriteRenderer.color = Color.yellow;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public bool TimerOver(float time)
    {
        return _timer > time;
    }

    public void TagChange1()
    {
        gameObject.tag = "Hit1";
    }
    public void TagChange2()
    {
        gameObject.tag = "Hit2";
    }
    public void PauseResume(bool pause)
    {
        if(pause)
        {
            _paused = true;
            _anim.speed = 0;
        }
        else
        {
            _paused = false;
            _anim.speed = 1;
        }
    }
}
