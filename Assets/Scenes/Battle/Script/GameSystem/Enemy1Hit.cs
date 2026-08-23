using UnityEngine;
using System.Collections;

public class Enemy1Hit : HitSponer
{
    
    public override IEnumerator Sphere()
    {

        while (true)
        {
            int num = Random.Range(0, 3);

            if (num == 0)
            {
                //上
                _anim.SetTrigger("Up");
                Instantiate(_hitSphere, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

            }
            else if (num == 1)
            {
                //中
                _anim.SetTrigger("Middle");
                Instantiate(_hitSphere, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            }
            else if (num == 2)
            {
                // 下
                _anim.SetTrigger("Down");
                Instantiate(_hitSphere, new Vector3(transform.position.x, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //左下
                _anim.SetTrigger("Down");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }


            yield return new WaitForSeconds(3);

        }
    }

    }
