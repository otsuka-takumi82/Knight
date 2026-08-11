using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField,Header("プレイヤー攻撃力")]
    public float _playerDamage;
    [SerializeField, Header("プレイヤー剣")]
    public SpriteRenderer _currentWepon;
    [SerializeField]
    public float _maxHp;
    [SerializeField]
    public float _maxStamina;
    [SerializeField]
    private float _staggerPile = 1;
    [SerializeField]
    private float _attackCoolTime = 1;
    

    public float _currentHp;
    public float _currentStamina;
    public int _currentHarb;
    private int _currentHighHarb;
    private Wepon _wepon;
    private BattleUIManager _uiManager;
    private EnemyHelth _enemy;
    private GameManager _gameManager;
    public bool _stagging;
    public bool _canAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiManager = FindFirstObjectByType<BattleUIManager>();
        _enemy = FindFirstObjectByType<EnemyHelth>();
        _gameManager = FindFirstObjectByType<GameManager>();
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
                _playerDamage *= 2;
            }
           
            //_currentHarb = _gameManager._harb;
            _currentHighHarb = _gameManager._highHarb;
            if (_gameManager.State(GameManager.PlayerState.Power))
            {
                _playerDamage *= 2;
            }
        }
        _currentWepon.sprite = _gameManager._swordImage[_gameManager._currentEquipped];
        
        
        _canAttack = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_canAttack);
        if (! _stagging )
        {
            if (_currentStamina < _maxStamina)
            {
                _currentStamina += Time.deltaTime * 0.5f;
                _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
                ShowStamina();
            }
        }

    }
    private void OnDestroy()
    {
        _gameManager.AddRepair(-1);
    }

    public void PlayerModifyHelth()
    {
        _currentHp += _enemy._damage * _staggerPile;
        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
        ShowHP();
        if( _currentHp <= 0 )
        {
            Debug.Log("Dead");
        }
    }

    public void ModifyStamina()
    {

        _currentStamina += _enemy._damage;
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
        yield return new WaitForSeconds(5);
        _stagging = false;
        _staggerPile = 1;
        _currentStamina = _maxStamina;
        ShowStamina();
    }

    public IEnumerator AttackCoolTime()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCoolTime);
        _canAttack = true;
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

}
