using System.Collections;
using UnityEngine;
using static GameManager;

public class UnCreated : MonoBehaviour
{
    [SerializeField]
    float _destroyTime = 1f;
    GameManager _gm;
    Animator _anim;
    float _timer;
    bool _pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _gm = FindFirstObjectByType<GameManager>();
        StartCoroutine(Dest());
    }
    void OnEnable()
    {
        _gm._pauseReseum += PauseResume;
    }
    void OnDisable()
    {
        _gm._pauseReseum -= PauseResume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseResume(bool pause)
    {
        if(_anim != null)
        {
            if (pause)
            {
                _pause = true;
                _anim.speed = 0;
            }
            else
            {
                _pause = false;
                _anim.speed = 1;
            }
        }
        
    }
    public IEnumerator Dest()
    {
        while (_timer < _destroyTime)
        {
            if (!_pause)
            {
                _timer += Time.deltaTime;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
