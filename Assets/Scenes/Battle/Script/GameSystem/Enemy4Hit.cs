using UnityEngine;
using System.Collections;

public class Enemy4Hit : HitSponer, ICounter
{
    [SerializeField] float _diley;
    [SerializeField]GameObject _fastSphire;
    [SerializeField]GameObject _bigSphire;
    [SerializeField, Header("カウンターHit")] GameObject _counterSphere;
    
    public override IEnumerator Sphere()
    {
        yield return new WaitForSeconds(_waitNum);
        while (true)
        {
            int num = Random.Range(0, 5);
            if(_enemy._currentHp > _enemy._maxHp / 2 && !_player._stagging)
            {
                if(num == 4)num = 0;
            }
            _anim.speed = _animSpeed;
            if (num == 0)
            {
                //右
                _attack = AttackState.Stamina;
                _anim.SetTrigger("Right");
                Instantiate(_fastSphire, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity);

            }
            else if (num == 1)
            {
                //左
                _anim.SetTrigger("Left");
                Instantiate(_fastSphire, new Vector3(transform.position.x + -3, transform.position.y, transform.position.z), Quaternion.identity);

            }
            else if (num == 2)
            {
                //ブラフ左
                _anim.SetTrigger("BrafLeft");
                Instantiate(_hitSphere, new Vector3(transform.position.x + -3, transform.position.y, transform.position.z), Quaternion.identity);
                
                yield return new WaitForSeconds(1.5f);
                _isBraff = true;
                yield return new WaitForSeconds(0.5f);
                _isBraff = false;
                //右
                _anim.SetTrigger("Right");
                Instantiate(_fastSphire, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //カウンター
                _anim.SetTrigger("Counter");
                Instantiate(_counterSphere, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else if (num == 4)
            {
                //カウンター
                _anim.SetTrigger("Counter");
                Instantiate(_bigSphire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }

            if (num == 3)
            {
                _diley = 4;
            }
            else if (num == 4)
            {
                _diley = 8;
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
        
        int num = Random.Range(0, 2);
        if(num == 0)
        {
            _anim.SetTrigger("CounterAttack");
            Instantiate(_fastSphire, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
       else if(num == 1)
        {
            _anim.SetTrigger("CounterAttack");
            Instantiate(_fastSphire, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), Quaternion.identity);
        }
        
    }

}
