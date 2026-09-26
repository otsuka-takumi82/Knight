using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HitSponer : MonoBehaviour
{
    [SerializeField, Header("円ヒットボックス")]
    public GameObject _hitSphere;
    [SerializeField, Header("特殊ボール")]
    public GameObject[] _ball;
    [SerializeField, UnitHeaderInspectable("反応コメ")]
    public GameObject _commentObject;
    [SerializeField]private int _enemyNum;
    [SerializeField]private string _comment;
    [SerializeField]public string[] _activeComment;
    private Text _commentBox;
    public enum AttackState
    {
        Nomal,
        Stamina,
        Damage
    };
    public enum EnemyState
    {
        Nomal,
        Up,
        Down,
    };
    public AttackState _attack = AttackState.Nomal;
    public Animator _anim;
    public float _animSpeed = 1;
    private Coroutine _sphereCor;
    public EnemyHelth _enemy;
    public BattleUIManager _ui;
    public Player _player;
    public float _waitNum = 3;
    public float _powerPile = 2;
    private bool _isOne;
    bool _one = true;
    bool _isComment = true;
    public bool _isPause;
    public bool _isBraff;
    private GameManager _gm;
    SpriteRenderer _ren;
    private void Awake()
    {
        _ren = GetComponent<SpriteRenderer>();
        _ui = FindFirstObjectByType<BattleUIManager>();
        _player = FindFirstObjectByType<Player>();
        _gm = FindFirstObjectByType<GameManager>();
        _enemy = FindFirstObjectByType<EnemyHelth>();
        _anim = GetComponent<Animator>();
        if (_gm._currentFight != _enemyNum)
        {
            gameObject.SetActive(false);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sphereCor = StartCoroutine(Sphere());

    }
    private void OnEnable()
    {
        _gm._pauseReseum += PauseReseum;
    }
    private void OnDisable()
    {
        _gm._pauseReseum -= PauseReseum;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_sphereCor);
        if(!_isPause)
        {
            if (_enemy._stagging || _enemy.Died())
            {
                if (_sphereCor != null)
                {
                    StopCoroutine(_sphereCor);
                    _sphereCor = null;
                }
                _isOne = true;
            }
            else
            {
                if (_isOne)
                {
                    _sphereCor = StartCoroutine(Sphere());
                    _isOne = false;
                }

            }

            
        }
        if (_one && _enemy._currentHp <= _enemy._maxHp / 2)
        {
            _ui.CommentActive();
            _commentBox = GameObject.FindGameObjectWithTag("Coment").GetComponent<Text>();
            _commentBox.text = _comment;
            _one = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && _isComment)
        {
            Agree();
            _isComment = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _isComment)
        {
            DisAgree();
            _isComment = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _isComment)
        {
            Nomal();
            _isComment = false;
        }
        
    }

    public virtual IEnumerator Sphere()
    {
        yield return new WaitForSeconds(_waitNum);
        while (true)
        {
            
            int num = Random.Range(0, 4);

            if (num == 0)
            {
                //右上
                _anim.SetTrigger("RightUP");
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z), Quaternion.identity);

            }
            else if (num == 1)
            {
                //左上
                _anim.SetTrigger("LeftUP");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + 2, transform.position.z), Quaternion.identity);

            }
            else if (num == 2)
            {
                // 右下
                _anim.SetTrigger("RightDown");
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //左下
                _anim.SetTrigger("LeftDown");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }

            float waitNum = Random.Range(3, 6f);
            _waitNum = waitNum;
            yield return new WaitForSeconds(waitNum);

            if (_isPause)
            {
                yield return null;
                continue;
            }


        }
        
    }
    public void Nomal()
    {
        _ui.CommentActive();
    }
    public virtual void Agree()
    {
        _enemy._buff.color = Color.white;
        _enemy._buff.sprite = _enemy._buffSprite[1];
        Transform parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        GameObject obj = Instantiate(_commentObject,parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(150f, 150f);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = _activeComment[0];
        _ren.color = Color.yellow;
        _player._getPile *= 2;
        _ui.CommentActive();
    }
    public virtual void DisAgree()
    {
        _enemy._buff.color = Color.blue;
        _enemy._buff.sprite = _enemy._buffSprite[1];
        Transform parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        GameObject obj = Instantiate(_commentObject, parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(150f, 150f);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = _activeComment[1];
        _ren.color = Color.blue;
        _player._getPile *= 0.5f;
        _ui.CommentActive();
    }

    public void PauseReseum(bool paused)
    {
        if(paused)
        {
            _isPause = true;
            if (_sphereCor != null)
            {
                _isPause = true;
                StopCoroutine(_sphereCor);
                if (_anim != null) _anim.speed = 0f;
                _isOne = false;
            }
        }
        else
        {
            _isPause = false;
            if (_anim != null) _anim.speed = _animSpeed;
            _isOne = true;
        }
    }
}
