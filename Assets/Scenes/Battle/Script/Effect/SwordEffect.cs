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

    private EnemyHelth _enemyHelth;
    private Player _player;

    public bool _isCombos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
        _audio = GetComponentInParent<AudioSource>();
    }

    private void OnDestroy()
    {
        Debug.Log("破壊");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RightUp"))
        {
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            Hibana(transform.position);
            _audio.PlayOneShot(_se[0]);
        }
        if (collision.gameObject.CompareTag("GoodBall"))
        {
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
                Debug.Log(circle._hp);
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
            _player._noCombo = false;
            _isCombos = false;
        }
        else if(_player._noCombo && !_isCombos)
        {
            _player._commboNum = 0;
        }
    }
    public void Hibana(Vector3 hibanapos)
    {
        Instantiate(_hibana, hibanapos, Quaternion.identity);
    }
}
