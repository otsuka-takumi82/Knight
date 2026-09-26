using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHelth : MonoBehaviour
{
    [SerializeField, Header("バフ画像")] public Sprite[] _buffSprite;
    [SerializeField, Header("バフ")] public Image _buff;
    [SerializeField]
    public string _name;
    [SerializeField]
    public float _maxHp;
    public float _currentHp;
    [SerializeField,Header("敵攻撃力")]
    public float _damage;
    [SerializeField]
    public float _maxStamina;
    public float _currentStamina;
    [SerializeField]
    private float _staggerPile = 1;
    [SerializeField]
    public float _enemyScore = 10;
    [SerializeField]
    private GameObject _result;
    [SerializeField]
    public Sprite _enemyImage;

    private BattleUIManager _uiManager;
    private Player _player;
    public bool _stagging;
   
    private Animator _anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _uiManager = FindFirstObjectByType<BattleUIManager>();
        _player = FindFirstObjectByType<Player>();
        _anim = GetComponent<Animator>();
        _currentHp = _maxHp;
        _currentStamina = _maxStamina;
        ShowHP();
        ShowStamina();
    }
    void Start()
    {
        _buff.sprite = _buffSprite[0];

    }

    private void Update()
    {

    }

    public void ModifyHelth(float amount)
    {
        _currentHp += amount * _staggerPile;
        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
        ShowHP();
        if ( _currentHp <= 0 )
        {
            _anim.Play("Died");
            StartCoroutine(StartResult());
        }
        else if(_player._isDead)
        {

        }
    }

    public void ModifyStamina(float amount)
    {
        _currentStamina += amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        ShowStamina();
        if(!_stagging)
        {
            if (_currentStamina <= 0)
            {
                StartCoroutine(Stagger());
            }
        }
        
    }

    public void ShowHP()
    {
        
        _uiManager.EnemyHPUI(_currentHp, _maxHp);
    }

    public void ShowStamina()
    {

        _uiManager.EnemyStaminaUI(_currentStamina, _maxStamina);
    }

    public void PlayerDamage(float pile = 1)
    {
        ModifyHelth(_player._playerDamage * pile * _player._skillPile);
    }
    public void PlayerStamina(float pile = 1)
    {
        ModifyStamina(_player._playerDamage * pile);
    }

    public IEnumerator Stagger()
    {
        _stagging = true;
        _anim.ResetTrigger("Knock");
        _anim.SetTrigger("Stagger");
        _staggerPile *= 4f;
        yield return new WaitForSeconds(5);
        _stagging = false;
        _staggerPile /= 4;
        _currentStamina = _maxStamina;
        ShowStamina();
    }
    public IEnumerator StartResult()
    {
        yield return new WaitForSeconds(1);
        _result.SetActive(true);

    }

    public bool Died()
    {
        return _currentHp <= 0;
    }

    public void Knock()
    {
        if(!_stagging)
        {
            _anim.SetTrigger("Knock");
        }
        
    }

    
}
