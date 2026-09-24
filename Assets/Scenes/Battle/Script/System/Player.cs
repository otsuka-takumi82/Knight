using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField,Header("プレイヤー攻撃力")]
    public float _playerDamage;
    [SerializeField, Header("プレイヤー剣")]
    public SpriteRenderer _currentWepon;
    [SerializeField]
    public float _maxHp;
    [SerializeField]
    public Text _combo;
    public float _getPile = 1;
    public float _skillPile = 1;
    [SerializeField]
    public float _maxStamina;
    [SerializeField]
    private float _staggerPile = 1;
    [SerializeField, Header("スタッガー")]
    public AudioClip _breath;
    [SerializeField]
    public float _attackCoolTime = 0.5f;
    [SerializeField]
    public float _currentCoolTime = 0.5f;
    public DirectionAttack.AttackType _playerAttackType = DirectionAttack.AttackType.RightUp;
    

    public float _currentHp;
    public float _currentStamina;
    public int _currentHarb;
    public int _commboNum;
    public int _saveCommbo;
    [SerializeField]public float _addScore = 10;
    private int _currentHighHarb;
    private Wepon _wepon;
    private BattleUIManager _uiManager;
    private EnemyHelth _enemy;
    private GameManager _gameManager;
    public bool _stagging;
    public bool _canAttack;
    public bool _isDead;
    public bool _isShield = true;
    public bool _shieldOne = true;
    bool _paused;
    Animator _anim;
    public AudioSource _audio;
    [SerializeField] Animator _animShield;
    private void Awake()
    {
        _uiManager = FindFirstObjectByType<BattleUIManager>();
        _enemy = FindFirstObjectByType<EnemyHelth>();
        _gameManager = FindFirstObjectByType<GameManager>();
        _anim = GetComponentInChildren<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHp = _maxHp;
        _currentStamina = _maxStamina;
        if (_gameManager != null)
        {
            _wepon = _gameManager.CurrentWepon;
            _playerDamage = _wepon._weponPower;
            if (_wepon._repairPal == 0)
            {
                _playerDamage *= 0.5f;
            }
            else if (_wepon._repairPal == 2)
            {
                _playerDamage *= 1.2f;
            }
           
            //_currentHarb = _gameManager._harb;
            _currentHighHarb = _gameManager._highHarb;
            if (_gameManager.State(GameManager.PlayerState.Power))
            {
                _playerDamage *= 1.2f;
            }
        }
        _currentWepon.sprite = _gameManager._swordImage[_gameManager._currentEquipped];
        
            _canAttack = true;
        
        
    }
    void OnEnable()
    {
        _gameManager._pauseReseum += PauseReseum;
    }
    void OnDisable()
    {
        _gameManager._pauseReseum -= PauseReseum;
    }

    // Update is called once per frame
    void Update()
    {
        _combo.text = $"{_commboNum.ToString("0")}combo";
        Debug.Log(_saveCommbo);
        if (! _stagging )
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if( _shieldOne )
                {
                    
                    _isShield = true;
                    _animShield.SetBool("Shield", true);
                    _shieldOne = false;
                }
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift) && !_shieldOne)
            {
                _isShield = false;
                _animShield.SetBool("Shield", false);
                _shieldOne = true;
            }
            if (_currentStamina < _maxStamina)
            {
                if(!_isShield)
                {
                    _currentStamina += Time.deltaTime * 0.5f;
                }
                _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
                ShowStamina();
            }
        }
        else
        {
            _isShield = false;
            _animShield.SetBool("Shield", false);
        }

    }
    private void OnDestroy()
    {
        _gameManager.AddRepair(-0);
    }

    public void PlayerModifyHelth(float pile = 1)
    {
        float damage = _enemy._damage * pile * _getPile * _staggerPile;
        _currentHp += damage;
        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
        ShowHP();
        if( _currentHp <= 0 )
        {
            _isDead = true;
            if(_isDead)
            {
                FindFirstObjectByType<SceneLoader>().LoadTimeAdd();
                //_isDead = false;
            }
            
        }
    }

    public void ModifyStamina(float num = 1)
    {

        _currentStamina += _enemy._damage * num;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        ShowStamina();
        if (!_stagging)
        {
            if (_currentStamina <= 0)
            {
                
                StartCoroutine(Stagger());
            }
        }

    }

    public void ShowHP()
    {

        _uiManager.PlayerHPUI(_currentHp, _maxHp);
    }

    public void ShowStamina()
    {

        _uiManager.PlayerStaminaUI(_currentStamina, _maxStamina);
    }

    public IEnumerator Stagger()
    {
        _staggerPile = 1.5f;
        _stagging = true;
        _audio.PlayOneShot(_breath);
        yield return new WaitForSeconds(5);
        _stagging = false;
        _staggerPile = 1;
        _currentStamina = _maxStamina;
        ShowStamina();
    }

    public IEnumerator AttackCoolTime(float coolPile = 1f)
    {
        _canAttack = false;
        yield return new WaitForSeconds(_currentCoolTime * coolPile);
        _canAttack = true;
        _currentCoolTime = _attackCoolTime;
    }

    public void AddHelth(float helth)
    {
            _currentHp += helth;
            ShowHP();
    }
    public void AddStamina(float helth)
    {
        _currentStamina += helth;
        ShowStamina();
    }
    public void PauseReseum(bool paused)
    {
        if (paused)
        {
            _anim.speed = 0;
            _animShield.speed = 0;
        }
        else
        {
            _anim.speed = 1;
            _animShield.speed = 1;
        }

    }

    public float AddScore()
    {
        return _saveCommbo * _addScore;
    }
}
