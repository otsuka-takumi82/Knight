using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _events;
    [SerializeField]Transform _target;
    Player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_player._stagging)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    StartCoroutine(Skill());
            //    _events[0].Invoke();
            //    _target.position = new Vector3(0, 0, 0);
            //}
        }
        
    }

    public void Skill()
    {
        if (!_player._stagging)
        {
            StartCoroutine(SkillCol());
            _events[0].Invoke();
            _target.position = new Vector3(0, 0, 0);
        }
    }
    public IEnumerator SkillCol()
    {
        _player._skillPile = 5;
        yield return new WaitForSeconds(5f);
        _player._skillPile = 1;
    }
}
