using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MaceControler : MonoBehaviour
{
    [SerializeField,Header("決定")] Button _okButton;
    [SerializeField,Header("タイマー")] Text _timerText;
    [SerializeField, Header("events")]
    UnityEvent[] _events;
    float _timer;
    [SerializeField, Header("マグマ")] GameObject[] _maguma;
    [SerializeField, Header("maxタイマー")] float _maxTimer;
    int _eventNum;
    bool _barnStop;
    bool _isShatter = true;
    bool _open = true;
    public Animator _maceAnim;
    Color _magmaColor;
    SwordControler _sw;
    string _timerStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        _sw = FindFirstObjectByType<SwordControler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_eventNum == 0)
        {

        }
        else if (_eventNum == 1)
        {
            if( _open)
            {
                _events[0].Invoke();
                _open = false;
            }
            _timer += Time.deltaTime;
            float timer = _timer * 100;
            if (_timer < 3) _timerStr = $"{timer.ToString("0")}C°";
            _timerText.text = _timerStr;
            if (_timer >= 3)
            {
                _timerStr = "???C°";
                if (_isShatter)
                {
                    _maceAnim.SetBool("Open", false);
                    _isShatter = false;
                }
            }
            if (_barnStop)
            {
                if (!_isShatter)
                {
                    if (!_maceAnim.GetBool("Open"))
                    {
                        _maceAnim.SetBool("Open", true);
                    }
                }
                else
                {

                }
                _timerStr = $"{timer.ToString("0")}C°";
                _timerText.text = _timerStr;
                _maguma[0].GetComponent<Animator>().speed = 0;
                _magmaColor = _maguma[0].GetComponent<SpriteRenderer>().color;
                _maguma[2].GetComponent<SpriteRenderer>().color = _magmaColor;
                _maguma[1].GetComponent<Image>().color = _magmaColor;
                _events[1].Invoke();
                if (Mathf.Abs(_maxTimer - _timer) <= 1)
                {
                    _sw._currentWepon._repairPal = 5;
                }
                else if (Mathf.Abs(_maxTimer - _timer) <= 2)
                {
                    _sw._currentWepon._repairPal = 2;
                }
                else if(Mathf.Abs(_maxTimer - _timer) > 2)
                {
                    _sw._currentWepon._repairPal = 1;
                }
               
                _eventNum++;
                _barnStop = false;
            }
        }
        else if (_eventNum == 2)
        {
            StartCoroutine(_sw.FinishMake());
            _eventNum++;
        }
        else if (_eventNum == 3)
        {

        }
    }

    public void OKButton()
    {
        if(_eventNum  == 0)
        {
            _eventNum++;
        }
        else
        {
            _barnStop = true;
        }
        
    }
}
