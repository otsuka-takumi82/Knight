using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitSponer : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("円ヒットボックス")]
    private GameObject _hitSphere;

    private Coroutine _sphereCor;
    private EnemyHelth _enemy;
    private bool _isOne;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = FindFirstObjectByType<EnemyHelth>();
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
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z), Quaternion.identity);
            }
            else if (num == 1)
            {
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + 2, transform.position.z), Quaternion.identity);
            }
            else if (num == 2)
            {
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }

            yield return new WaitForSeconds(3f);
        }
        
    }
}
