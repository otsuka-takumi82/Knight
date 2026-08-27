using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitSponer : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("円ヒットボックス")]
    public GameObject _hitSphere;
    [SerializeField]private int _enemyNum;
    public enum AttackState
    {
        Nomal,
        Stamina,
        Damage
    };
    public AttackState _attack = AttackState.Nomal;
    public Animator _anim;
    public float _animSpeed = 1;
    private Coroutine _sphereCor;
    public EnemyHelth _enemy;
    public Player _player;
    public float _waitNum = 3;
    private bool _isOne;
    public bool _isPause;
    public bool _isBraff;
    private GameManager _gm;
    private void Awake()
    {
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
