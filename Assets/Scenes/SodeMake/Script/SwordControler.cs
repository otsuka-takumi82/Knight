using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SwordControler : MonoBehaviour
{
    [SerializeField, Header("熱")]
    private int _maxBarn;
    [SerializeField, Header("完成度")]
    private int _maxSordPal;
    [SerializeField, Header("完成度")]
    UnityEvent[] _events;

    bool _swordActive = true;
    private int _currentBarn;
    private int _currentPal;
    private GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindFirstObjectByType<GameManager>();

        //_currentBarn = _maxBarn;
        CheckBarn();
        CheckPal();

    }

    // Update is called once per frame
    void Update()
    {
        
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
        if( _currentBarn >= _maxBarn )
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
    public void CheckPal()
    {
        if (_swordActive)
        {
            if (_currentPal >= _maxSordPal)
            {
                //黒
                Debug.Log("完成！");
                _events[3].Invoke();
            }
            else if (_currentPal >= _maxSordPal * 0.75f)
            {
                //赤
                Debug.Log("あと少し！");
                _events[2].Invoke();
            }
            else if (_currentPal >= _maxSordPal * 0.5f)
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

    }
}
