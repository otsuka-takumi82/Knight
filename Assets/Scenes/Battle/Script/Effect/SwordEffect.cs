using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("火花")]
    GameObject _hibana;
    AudioSource _audio;
    [SerializeField,Header("効果音")]AudioClip[] _se;
    float[] _fibo = new float[2];
    private EnemyHelth _enemyHelth;
    private Player _player;

    public bool _isCombos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
        _audio = GetComponentInParent<AudioSource>();
        _fibo[1] = _player._addSkillPts;
    }

    private void OnDestroy()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RightUp"))
        {
            //if(_player._commboNum >= PlusFibo())
            //{
            //    _player.AddSkillPts(PlusFibo());
            //}
            if (_player._commboNum >= 20 && !_player._isSkill)
            {
                _player.AddSkillPts(_player._addSkillPts * 2);
            }
            else if (_player._commboNum >= 5 && !_player._isSkill)
            {
                _player.AddSkillPts(_player._addSkillPts);
            }
            
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            Hibana(transform.position);
            _audio.PlayOneShot(_se[0]);
        }
        if (collision.gameObject.CompareTag("GoodBall"))
        {
            if (!_player._isSkill)
            {
                _player.AddSkillPts(_player._skillMax / 5);
            }
            
            _player.ModifyStamina(-1);
            _audio.PlayOneShot(_se[2]);
            Hibana(collision.transform.position);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Hit2"))
        {
            if(!_player._isSkill)
            {
                _player.AddSkillPts(_player._addSkillPts * 2);
            }
            _isCombos = true;
            _enemyHelth.Knock();
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina(2);
            Hibana(transform.position);
            _audio.PlayOneShot(_se[1]);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Hit1"))
        {
            _enemyHelth.Knock();
            _enemyHelth.PlayerStamina();
            _player.ModifyStamina();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            _enemyHelth.Knock();
            _player.ModifyStamina();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("HitCounter"))
        {
            HitSponer sponer = FindFirstObjectByType<HitSponer>();
            if (sponer is ICounter counter)
            {
                counter.CounterAttack();
            }
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("HitBig"))
        {
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina(2);
            if (collision.gameObject.TryGetComponent<HItBigCircle>(out HItBigCircle circle))
            {
                circle._hp += _player._playerDamage;
                if (circle._hp <= 0)
                {
                    _enemyHelth.Stagger();
                    Destroy(collision.gameObject);
                }
            }
           
            
        }
    }
    public void GetCombo()
    {
        if (_player._noCombo && _isCombos)
        {
            _player._commboNum++;
        }
        else if(_player._noCombo && !_isCombos)
        {
            _player._commboNum = 0;
            _fibo[0] = 0;
            _fibo[1] = _player._addSkillPts;
        }
        _player._noCombo = false;
        _isCombos = false;
    }
    public void Hibana(Vector3 hibanapos)
    {
        Instantiate(_hibana, hibanapos, Quaternion.identity);
    }

    public void GetFibo()
    {
        float num3 = PlusFibo();
        _fibo[0] = _fibo[1];
        _fibo[1] = num3;
    }
    public float PlusFibo()
    {
       return _fibo[0] + _fibo[1];
    }
}
