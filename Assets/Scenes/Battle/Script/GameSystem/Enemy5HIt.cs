using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Enemy5Hit : HitSponer, ICounter
{
    [SerializeField] float _diley;
    [SerializeField] GameObject _fastSphire;
    [SerializeField] GameObject _bigSphire;
    [SerializeField, Header("カウンターHit")] GameObject _counterSphere;

    public override IEnumerator Sphere()
    {
        yield return new WaitForSeconds(_waitNum);
        while (true)
        {
            int num = Random.Range(0, 4);
            if (_enemy._currentHp > _enemy._maxHp / 2 && !_player._stagging)
            {
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
                _attack = AttackState.Nomal;
                _anim.SetTrigger("Middle");
                Instantiate(_fastSphire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else if (num == 3)
            {
                //カウンター
                _anim.SetTrigger("Counter");
                Instantiate(_counterSphere, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }

            if (num == 3)
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
        _attack = AttackState.Stamina;
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

}
