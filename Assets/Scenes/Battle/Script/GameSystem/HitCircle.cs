using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitCircle : MonoBehaviour,IPointerDownHandler
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _directionAttack = FindFirstObjectByType<DirectionAttack>();
        _anim = GetComponent<Animator>();
        if(_name != null)
        {
            _anim.Play(_name);
        }
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
        if (TimerOver(_maxTimer))
        {
            _player.PlayerModifyHelth();
            _player.ModifyStamina();
            Destroy(gameObject);
        }
        if (!_player._stagging)
        {
            if (TimerOver(_maxTimer * 0.75f))
            {
                TagChange2();
            }
            else if (TimerOver(_maxTimer * 0.5f))
            {
                TagChange1();
            }

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
