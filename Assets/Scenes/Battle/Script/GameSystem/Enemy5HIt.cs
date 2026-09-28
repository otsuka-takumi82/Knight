using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Enemy5Hit : HitSponer, ICounter
{
    [SerializeField] float _diley;
    [SerializeField] GameObject _fastSphire;
    [SerializeField] GameObject _bigSphire;
    [SerializeField, Header("カウンターHit")] GameObject _counterSphere;
    [SerializeField, Header("Goat2")] RuntimeAnimatorController _goat2;
    int _enemyState = 0;
    int _saveAttack;
    bool _saveOne;

    public override IEnumerator Sphere()
    {

        yield return new WaitForSeconds(_waitNum);
            while (true)
        {
            if (_enemyState == 1)
            {
                _anim.runtimeAnimatorController = _goat2;
                _enemyState = 2;
            }
            if ( _enemyState == 0)
            {
                if (_enemy._currentHp < _enemy._maxHp / 2)
                {
                    _saveOne = true;
                }
                int num = 0;
                if (!_saveOne)
                {
                    num = Random.Range(0, 4);
                }
                else
                {
                    num = 4;
                }
                
                if (_enemy._currentHp < _enemy._maxHp / 2)
                {
                    _enemyState = 1;
                }
                _anim.speed = _animSpeed;
                if (num == 0)
                {
                    //右
                    _attack = AttackState.Damage;
                    _anim.SetTrigger("Right");
                    Instantiate(_fastSphire, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity);

                }
                else if (num == 1)
                {
                    //左
                    _attack = AttackState.Damage;
                    _anim.SetTrigger("Left");
                    Instantiate(_fastSphire, new Vector3(transform.position.x + -3, transform.position.y, transform.position.z), Quaternion.identity);

                }
                else if (num == 2)
                {
                    //真ん中
                    _attack = AttackState.Stamina;
                    _anim.SetTrigger("Middle");
                    Instantiate(_fastSphire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
                else if (num == 3)
                {
                    //カウンター
                    _anim.SetTrigger("Counter");
                    Instantiate(_counterSphere, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
                else if (num == 4)
                {
                    //進化
                    _anim.SetTrigger("Change");
                }

                if (num == 3 || num == 4)
                {
                    _diley = 4;
                }
                else
                {
                    _diley = 2;
                }
                float waitNum = _diley;
                _waitNum = waitNum;
                if (_enemy._currentHp < _enemy._maxHp / 2)
                {
                    _saveOne = true;
                }
                yield return new WaitForSeconds(waitNum);

                if (_isPause)
                {
                    yield return null;
                    continue;
                }
            }
            else if(_enemyState == 2)
            {
                if(_enemy._stagging)
                {
                    _saveAttack = 0;
                }
                int num = 0;
                if (_saveAttack == 0)
                {
                    num = Random.Range(0, 4);
                }
                else if( _saveAttack == 1)
                {
                    num = 4;
                }
                else if (_saveAttack == 2)
                {
                    num = 5;
                }
                else if (_saveAttack == 3)
                {
                    num = 1;
                }
                else if (_saveAttack == 4)
                {
                    num = 2;
                }
                _anim.speed = _animSpeed;
                if (num == 0)
                {
                    //左
                    _attack = AttackState.Damage;
                    _anim.SetTrigger("Left");

                }
                else if (num == 1)
                {
                    //右
                    _attack = AttackState.Damage;
                    int num2 = Random.Range(1,3);
                    if(num2 == 1)
                    {
                        _anim.SetTrigger("Right");
                        _saveAttack = 1;
                    }
                    else if(num2 == 2)
                    {
                        _anim.SetTrigger("Right2");
                        _saveAttack = 2;
                    }
                    Instantiate(_fastSphire, new Vector3(3, 0, 0), Quaternion.identity);

                }
                else if (num == 2)
                {
                    //クロス
                    _attack = AttackState.Stamina;
                    _anim.SetTrigger("Combo3");
                    Instantiate(_fastSphire, new Vector3(0, 0, 0), Quaternion.identity);
                    _saveAttack = 0;
                }
                else if (num == 3)
                {
                    //噛みつき
                    int num2 = Random.Range(-3,0);
                    if(num2 == -1)
                    {
                        _anim.SetTrigger("Big");
                        Instantiate(_bigSphire, new Vector3(0, 0, 0), Quaternion.identity);
                    }
                    else
                    {
                        num = 2;
                    }
                }
                else if (num == 4)
                {
                    //combo左
                    _attack = AttackState.Stamina;
                    _anim.SetTrigger("Combo1");
                    Instantiate(_fastSphire, new Vector3(-3, 0, 0), Quaternion.identity);
                    int num2 = Random.Range(0, 2);
                    if (num2 == 0)
                    {
                        _saveAttack = 3;
                    }
                    else if (num2 == 1)
                    {
                        _saveAttack = 4;
                    }
                }
                else if (num == 5)
                {
                    //combo下
                    _anim.SetTrigger("Combo2");
                    Instantiate(_fastSphire, new Vector3(0, -2, 0), Quaternion.identity);
                    int num2 = Random.Range(0, 2);
                    if (num2 == 0)
                    {
                        _saveAttack = 3;
                    }
                    else if (num2 == 1)
                    {
                        _saveAttack = 4;
                    }
                }

                if (num == 3)
                {
                    _diley = 8;
                }
                else if(num == 1 || num == 4 || num == 5)
                {
                    _diley = 1;
                }
                else if(num == 0)
                {
                    _diley = 5;
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
            else
            {
                yield return null;
            }
            

        }
    }
    void ICounter.CounterAttack()
    {
        _attack = AttackState.Nomal;
        _anim.SetTrigger("CounterAttack");
        Instantiate(_fastSphire, new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z), Quaternion.identity);
        Instantiate(_fastSphire, new Vector3(transform.position.x + -3, transform.position.y + -2, transform.position.z), Quaternion.identity);
    }
    public override void Agree()
    {
        _enemy._buff.color = Color.white;
        _enemy._buff.sprite = _enemy._buffSprite[1];
        Transform parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        GameObject obj = Instantiate(_commentObject, parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(150f, 150f);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = _activeComment[0];
        _player._getPile *= 2f;
        _ui.CommentActive();
    }
    public override void DisAgree()
    {
        _enemy._buff.color = Color.white;
        _enemy._buff.sprite = _enemy._buffSprite[1];
        Transform parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        GameObject obj = Instantiate(_commentObject, parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(150f, 150f);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = _activeComment[1];
        _player._getPile *= 2;
        _ui.CommentActive();
    }

    public void Ball(int num)
    {
        int answer = 0;
        if (num == 0)
        {
            answer = 3;
        }
        else if (num == 1)
        {
            answer = -3;
        }
        Instantiate(_ball[num], new Vector3(answer, 0, 0), Quaternion.identity);
    }
    public void Sphire()
    {
        Instantiate(_hitSphere,new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void First()
    {
        Instantiate(_fastSphire,new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void Ite(GameObject obj)
    {
        Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
    }

}
