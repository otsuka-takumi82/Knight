using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitSponer : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("円ヒットボックス")]
    private GameObject _hitSphere;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Sphere());
    }

    // Update is called once per frame
    void Update()
    {
        
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
