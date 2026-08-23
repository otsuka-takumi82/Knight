using UnityEngine;
using System.Collections;

public class Enemy2Hit : HitSponer
{
    [SerializeField] float _diley;
    public override IEnumerator Sphere()
    {

        while (true)
        {
            int num = Random.Range(0, 3);
            _anim.speed = 3;
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
            //_diley = Random.Range(1, 3);
            //if(_diley > 1 && _diley < 3)
            //{
            //    _diley = 3;
            //}

            if(num == 1)
            {
                _diley = 1;
            }
            else
            {
                _diley = 2;
            }
            yield return new WaitForSeconds(_diley);

        }
    }

}
