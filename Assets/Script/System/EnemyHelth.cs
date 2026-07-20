using System.Collections;
using UnityEngine;

public class EnemyHelth : MonoBehaviour
{
    [SerializeField]
    private float _maxHp;
    private float _currentHp;
    [SerializeField]
    public float _damage;
    [SerializeField]
    private float _maxStamina;
    private float _currentStamina;
    [SerializeField]
    private float _staggerPile = 1;
    

    private UIManager _uiManager;
    private Player _player;
    public bool _stagging;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
    void Start()
    {
        
        _uiManager = FindFirstObjectByType<UIManager>();
        _player = FindFirstObjectByType<Player>();
        _currentHp = _maxHp;
        _currentStamina = _maxStamina;
        ShowHP();
        ShowStamina();
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
        ModifyHelth(_player._playerDamage * pile);
    }
    public void PlayerStamina(float pile = 1)
    {
        ModifyStamina(_player._playerDamage * pile);
    }

    public IEnumerator Stagger()
    {
        _staggerPile = 4f;
        _stagging = true;
        yield return new WaitForSeconds(5);
        _stagging = false;
        _staggerPile = 1;
        _currentStamina = _maxStamina;
        ShowStamina();
    }

    public bool Died()
    {
        return _currentHp <= 0;
    }

    
}
