using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitSponer : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("円ヒットボックス")]
    private GameObject _hitSphere;

    private Animator _anim;
    private Coroutine _sphereCor;
    private EnemyHelth _enemy;
    private bool _isOne;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = FindFirstObjectByType<EnemyHelth>();
        _anim = GetComponent<Animator>();
        _sphereCor = StartCoroutine(Sphere());
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemy._stagging || _enemy.Died())
        {
            if(_sphereCor != null )
            {
                StopCoroutine( _sphereCor );
                _sphereCor = null;
            }
            _isOne = true;
        }
        else 
        {
            if( _isOne)
            {
                _sphereCor = StartCoroutine(Sphere());
                _isOne = false;
            }
            
        }
    }

    public IEnumerator Sphere()
    {
        
        while (true)
        {
            int num = Random.Range(0, 4);
            
            if (num == 0)
            {
                //右上
                _anim.SetTrigger("RightUP");
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z), Quaternion.identity);
                
            }
            else if (num == 1)
            {
                //左上
                _anim.SetTrigger("LeftUP");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + 2, transform.position.z), Quaternion.identity);
                
            }
            else if (num == 2)
            {
                // 右下
                _anim.SetTrigger("RightDown");
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //左下
                _anim.SetTrigger("LeftDown");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }

            
            yield return new WaitForSeconds(3f);

        }
        
    }

    
}
