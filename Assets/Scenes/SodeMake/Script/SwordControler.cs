using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SwordControler : MonoBehaviour
{
    [SerializeField, Header("熱")]
    private int _maxBarn;
    [SerializeField, Header("完成度")]
    private int _maxSordPal;
    [SerializeField, Header("鍛冶イベント")]
    UnityEvent[] _events;

    bool _swordActive = true;
    private int _currentBarn;
    private int _currentPal;
    private Wepon _currentWepon;
    private int _weponNum;
    private GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindFirstObjectByType<GameManager>();
        StartDefaultSword(_gameManager._currentMake);
        //_currentBarn = _maxBarn;
        //CheckBarn();
        //CheckPal();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDefaultSword(int num)
    {
        _weponNum = num;
        _currentWepon = _gameManager._wepon[num];
        if (!_currentWepon._isCrafted)
        {
            _currentPal = 0;
            CheckBarn();
            CheckPal();
        }
        else if (_currentWepon._isCrafted)
        {
            if (_currentWepon._repairPal == 0)
            {
                _currentPal = 5;
            }
            else if (_currentWepon._repairPal == 1)
            {
                _currentPal = 7;
            }
            else if (_currentWepon._repairPal == 2)
            {
                _currentPal = _maxSordPal;
                Debug.Log("完璧だ");
            }
            CheckBarn();
            CheckPal();
        }
    }
   
    public void Out()
    {
        _currentBarn += 1;
        CheckBarn();
        CheckPal();
    }

    public void Hit()
    {
        _currentBarn += 1;
        _currentPal++;
        CheckBarn();
        CheckPal();
    }

    public void CheckBarn()
    {
        if (_swordActive)
        {
            if (_currentBarn >= _maxBarn)
            {
                //黒
                Debug.Log("黒");
                _spriteRenderer.color = Color.black;
                _swordActive = false;

            }
            else if (_currentBarn >= _maxBarn * 0.75f)
            {
                //赤
                Debug.Log("赤");
                _spriteRenderer.color = Color.red;
            }
            else if (_currentBarn >= _maxBarn * 0.5f)
            {
                //オレンジ
                Debug.Log("オレンジ");
                _spriteRenderer.color = Color.yellow;
            }
            else
            {
                //黄色
                Debug.Log("黄色");
                _spriteRenderer.color = Color.white;
            }
        }
        
        

    }
    public void CheckPal()
    {
        if (_swordActive)
        {
            if (_currentPal >= _maxSordPal)
            {
                //黒
                Debug.Log("完成！");
                _currentWepon._repairPal = 2;
                _events[3].Invoke();
                if (!_currentWepon._isCrafted)
                {
                    _currentWepon._isCrafted = true;
                    _currentWepon._repairPal = 2;
                }
            }
            else if (_currentPal >= 7)
            {
                //赤
                Debug.Log("あと少し！");
                _currentWepon._repairPal = 1;
                _events[2].Invoke();
            }
            else if (_currentPal >= 5)
            {
                //オレンジ
                Debug.Log("まだまだ！！");
                _events[1].Invoke();
            }
            else
            {
                //黄色
                Debug.Log("もっと！");
                _events[0].Invoke();

            }
        }
        else
        {
            Debug.Log("失敗！");
        }
        _gameManager._wepon[_weponNum] = _currentWepon;
    }
}
