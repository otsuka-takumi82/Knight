using UnityEngine;
using System.Collections;

public class Enemy3Hit : HitSponer, ICounter
{
    [SerializeField] float _diley;
    [SerializeField,Header("カウンターHit")] GameObject _counterSphere;
    public override IEnumerator Sphere()
    {
        yield return new WaitForSeconds(_waitNum);
        while (true)
        {
            int num = Random.Range(0, 5);
            _anim.speed = _animSpeed;
            if (num == 0)
            {
                //右上
                _attack = AttackState.Stamina;
                _anim.SetTrigger("RightUp");
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z), Quaternion.identity);

            }
            else if (num == 1)
            {
                //左上
                _anim.SetTrigger("LeftUp");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -1, transform.position.y + 1, transform.position.z), Quaternion.identity);

            }
            else if (num == 2)
            {
                // 右下
                _anim.SetTrigger("RightDown");
                _attack = AttackState.Stamina;
                Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //左下
                _anim.SetTrigger("LeftDown");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
            }
            else if (num == 4)
            {
                //カウンター
                _anim.SetTrigger("Counter");
                Instantiate(_counterSphere, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            //_diley = Random.Range(1, 3);
            //if(_diley > 1 && _diley < 3)
            //{
            //    _diley = 3;
            //}

            if (num == 0 || num == 2)
            {
                _diley = 1;
            }
            else if (num == 4)
            {
                _diley = 4;
            }
            else
            {
                _diley = 2;
            }
            float waitNum = _diley;
            _waitNum = waitNum;
            yield return new WaitForSeconds(waitNum);

            if (_isPause)
            {
                yield return null;
                continue;
            }

        }
    }
    void ICounter.CounterAttack()
    {
        FindFirstObjectByType<Player>().ModifyStamina();
        _anim.SetTrigger("CounterAttack");
        Instantiate(_hitSphere, new Vector3(transform.position.x + 3, transform.position.y + -2, transform.position.z), Quaternion.identity);
        Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y + 2, transform.position.z), Quaternion.identity);
    } 

}
