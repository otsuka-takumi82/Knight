using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GageControler : MonoBehaviour
{
    [SerializeField,Header("動く幅")] float _height;
    [SerializeField, Header("動く速さ")] float _speed;
    [SerializeField, Header("当たりゲージ")] GameObject _hit;
    [SerializeField, Header("火花")] GameObject _hibana;
    [SerializeField, Header("ハンマー")] GameObject _hummer;


    private Rigidbody2D _rb;
    SwordControler _sw;
    HummerControler _hm;
    bool _isBottom;
    bool _isHit;
    bool _isHummed = true;
    float _myTime;
    float _defaultSpeed; 
    private Vector3 _startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sw = FindFirstObjectByType<SwordControler>();
        _hm = FindFirstObjectByType<HummerControler>();
        _startPos = transform.position;
        _defaultSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        _myTime += Time.deltaTime * _speed;
        float newY = Mathf.PingPong(_myTime, _height);

        transform.position = new Vector3(_startPos.x, _startPos.y + newY, _startPos.z);

        if (transform.position.y >= 3.9f && !_isBottom)
        {
            Debug.Log("a");
            _sw.Out();
            _speed *= 0.75f;
            _isHummed = true;
            _isBottom = true; // 判定済みにする（連続実行を防止）
        }
        // ゲージが少し上に上がったら（0.1以上）、フラグをリセットして次の折り返しに備える
        else if (transform.position.y < 3.9f)
        {
            _isBottom = false;
        }

        if (transform.position.y <= _startPos.y + 0.1f)
        {
            _isHummed = true;
        }
        // ゲージが少し上に上がったら（0.1以上）、フラグをリセットして次の折り返しに備える
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hit")
        {
            
            _isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hit")
        {
            _isHit = false;
        }
    }

    public void Hit()
    {
        if (_isHummed)
        {
            if (_isHit)
            {
                _hummer.transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
                Debug.Log("Hit");
                Instantiate(_hibana, this.transform.position, Quaternion.identity);
                _hm.Hummer();
                _sw.Hit();
                _myTime = 0;
                _speed = _defaultSpeed;
                _hit.transform.position = new Vector3(_hit.transform.position.x, Random.Range(0, 3));
                _isHummed = true;
            }
            else
            {
                _hummer.transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
                _hm.Hummer();
                _sw.Out();
                _isHummed = false;
                Debug.Log("Out");
            }
        }

    }
}
