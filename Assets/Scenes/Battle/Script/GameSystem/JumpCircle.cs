using System.Collections;
using System.Linq;
using UnityEngine;

public class JumpCircle : MonoBehaviour
{
    int _num;
    Rigidbody2D rb;
    private GameManager _gameManager;
    Vector2 _save;
    [SerializeField]Vector2 _force;
    EnemyHelth _enemy;
    bool _skiled = true;
    Player _player;
    public enum CircleState
    {
        Var,
        Hor,
        Else
    }

    public CircleState _state = CircleState.Var;
    private void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _player = FindFirstObjectByType<Player>();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = FindFirstObjectByType<EnemyHelth>();
        rb = GetComponent<Rigidbody2D>();
        JumpCircle[] objs = FindObjectsByType<JumpCircle>(FindObjectsSortMode.None);
        if(objs.Length >= 2)
        {
            _force *= -1;
        }
        rb.AddForce (_force,ForceMode2D.Force);
    }
    void OnEnable()
    {
        _gameManager._pauseReseum += PauseReseum;
        _player._skill += Skill;
        
    }
    void OnDisable()
    {
        _gameManager._pauseReseum -= PauseReseum;
        _player._skill -= Skill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string str = "Wall";
        if (_state == CircleState.Hor) str = "Floor";
        if (collision.gameObject.CompareTag(str))
        {
            if (_num >= 1)
            {
                BallDisabled();
            }
            _num++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_player._isSkill && _skiled)
        {
            Skill();
            _skiled = false;
        }
    }
    public void PauseReseum(bool paused)
    {
        if (paused)
        {
            _save = rb.linearVelocity;
            rb.Sleep();
        }
        else
        {
            rb.WakeUp();
            rb.linearVelocity = _save;
        }

    }

    public void BallDisabled()
    {
        _enemy.ModifyStamina(_enemy._maxStamina / 10);
        Destroy(gameObject);
    }
    public IEnumerator SkillCol()
    {
        rb.linearVelocity *= 0.5f;
        while (true)
        {
            gameObject.tag = "GoodBall";
            GetComponent<SpriteRenderer>().color = Color.green;
            yield return null;
        }
    }

    public void Skill()
    {
        StartCoroutine(SkillCol());
    }
}
