using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using UnityEngine.Events;
using System.Xml;

public class Player : MonoBehaviour
{
    [SerializeField,Header("プレイヤー攻撃力")]
    public float _playerDamage;
    [SerializeField, Header("プレイヤー剣")]
    public SpriteRenderer _currentWepon;
    [SerializeField, Header("バフ画像")] public Sprite[] _buffSprite;
    [SerializeField, Header("プレイヤーバフ")] public Image _buff;
    [SerializeField, Header("兜")] public GameObject _armored;
    [SerializeField]
    public float _maxHp;
    [SerializeField]
    public Text _combo;
    public float _getPile = 1;
    public float _armorPile = 1;
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
    public float _saveCoolTime;
    public DirectionAttack.AttackType _playerAttackType = DirectionAttack.AttackType.RightUp;
    [SerializeField, Header("スキル演出")] public UnityEvent[] _events;
    [SerializeField,Header("スキルゲージMax")] public float _skillMax;
    public float _skillPts;
    [SerializeField, Header("スキルゲージ+")] public float _addSkillPts = 1;
    [SerializeField,Header("スキル時間")]public float _skillTime = 5;
    public float _skillTimer;
    public float _currentHp;
    public float _currentStamina;
    float _save;
    public int _currentHarb;
    public int _commboNum;
    public int _saveCommbo;
    [SerializeField]public float _addScore = 10;
    private int _currentHighHarb;
    private Wepon _wepon;
    private BattleUIManager _uiManager;
    private EnemyHelth _enemy;
    private GameManager _gameManager;
    public Action _skill;
    public Action _hitStop;
    public bool _stagging;
    public bool _canAttack;
    public bool _isDead;
    public bool _isShield;
    public bool _shieldOne = true;
    public bool _noCombo;
    public bool _isSkill;
    public bool _isHitStop;

    bool _yesSkill = true;
    Animator _anim;
    public AudioSource _audio;
    [SerializeField] Animator _animShield;
    private void Awake()
    {
        _uiManager = FindFirstObjectByType<BattleUIManager>();
        _gameManager = FindFirstObjectByType<GameManager>();
        _anim = GetComponentInChildren<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = FindFirstObjectByType<EnemyHelth>();
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
            else if (_wepon._repairPal >= 2)
            {
                _playerDamage *= 1.2f;
            }
           
            //_currentHarb = _gameManager._harb;
            _currentHighHarb = _gameManager._highHarb;
            if (_gameManager.State(GameManager.PlayerState.Power))
            {
                _playerDamage *= 1.2f;
                _buff.sprite = _buffSprite[1];
            }
            else if (_gameManager.State(GameManager.PlayerState.Nomal))
            {
                _playerDamage *= 1.2f;
                _buff.sprite = _buffSprite[0];
            }
            if (_gameManager._wepon[_gameManager._currentEquipped]._weponState == WeponEnum.Sword)
            {
                _anim.speed *= 1;
            }
            else if (_gameManager._wepon[_gameManager._currentEquipped]._weponState == WeponEnum.Mace)
            {
                _anim.speed *= 0.75f;
            }
        }
        _currentWepon.sprite = _gameManager._swordImage[_gameManager._currentEquipped];
        
            _canAttack = true;
        _save = _anim.speed;

        if(_gameManager._isArmored)
        {
            _armorPile = 0.5f;
            _armored.SetActive(true);
        }
    }
    void OnEnable()
    {
        _gameManager._pauseReseum += PauseReseum;
        _skill += Skill;
        _hitStop += HitStop;
    }
    void OnDisable()
    {
        _gameManager._pauseReseum -= PauseReseum;
        _skill -= Skill;
        _hitStop -= HitStop;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_armorPile);
        if(_isSkill)
        {
            AddStamina(10);
        }
        if(_commboNum >= 20)
        {
            _combo.color = Color.red;
        }
        else if (_commboNum >= 5)
        {
            _combo.color = Color.yellow;
        }
        else
        {
            _combo.color = Color.green;
        }
        _combo.text = $"{_commboNum.ToString("0")}combo";
        if (! _stagging )
        {
            if(SkillMax())
            {
                if(_yesSkill)
                {
                    _events[3].Invoke();
                    _yesSkill = false;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _skill.Invoke();
                }
            }
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
        _gameManager.AddRepair(-1);
    }

    public void PlayerModifyHelth(float pile = 1)
    {
        float damage = _enemy._damage * pile * _getPile * _staggerPile * _armorPile;
        _currentHp += damage;
        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
        ShowHP();
        if( _currentHp <= 0 )
        {
            _isDead = true;
            if(_isDead)
            {
                SceneLoader scene = FindFirstObjectByType<SceneLoader>();
                if (_enemy._name == "Goat")
                {
                    StartCoroutine(scene.NoTimeSceneLoad("RoomScene"));
                }
                else
                {
                    scene.LoadTimeAdd();
                }                //_isDead = false;
            }
            
        }
    }

    public void ModifyStamina(float pile = 1)
    {
        if(!_isSkill)
        {
            _currentStamina += _enemy._damage * pile * _armorPile;
            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
            ShowStamina();
        }
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
            if(!_isHitStop && !_isSkill)
            {
                _save = _anim.speed;
            }
            _anim.speed = 0;
            _animShield.speed = 0;
        }
        else
        {
            _anim.speed = _save;
            _animShield.speed = _save;
        }

    }
    public float AddScore()
    {
        return _saveCommbo * _addScore;
    }
    public float GetSkillPts(float pile = 1)
    {
        return _skillPts * pile; 
    }
    public void AddSkillPts(float skill)
    {
        _skillPts = Mathf.Clamp(_skillPts + skill, 0f, _skillMax);
        _uiManager.PlayerSkillUI(_skillPts, _skillMax);
    }
    public bool SkillMax()
    {
        return _skillPts >= _skillMax;
    }

    public IEnumerator SkillCol()
    {
        _skillPts = 0;
        _saveCoolTime = _attackCoolTime;
        _attackCoolTime = 0.25f;
        _isSkill = true;
        _events[0].Invoke();
        _save = _anim.speed;
        _anim.speed *= 1.5f;
        yield return new WaitForSeconds(_skillTime);
        PlayerSkill skill = GetComponent<PlayerSkill>();
        _events[2].Invoke();
        while(_skillTimer <= 2)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                skill.Skill();
                break;
            }
            _skillTimer += Time.deltaTime;
            yield return null;
        }
        _yesSkill = true;
        _attackCoolTime = _saveCoolTime;
        _skillTimer = 0f;
        _events[1].Invoke();
        yield return new WaitForSeconds(1);
        _isSkill = false;
        _anim.speed = _save;
    }
    public void Skill()
    {
        StartCoroutine(SkillCol());
    }
    public void HitStop()
    {
        StartCoroutine(HitStopCol());
    }
    public IEnumerator HitStopCol()
    {
        if(!_isHitStop && !_isSkill)
        {
            _save = _anim.speed;
        }
        _isHitStop = true;
        _anim.speed *= 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);
        _anim.speed = _save;
        _isHitStop = false;
        
    }
}
