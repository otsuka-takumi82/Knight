using System.Collections;
using UnityEngine;

public class HibanaSodeMake : MonoBehaviour
{
    Player _player;
    GameManager _gameManager;
    float _timer;
    bool _isPause;
    private void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player = FindFirstObjectByType<Player>();
        }
    }
    private void OnEnable()
    {
        _gameManager._pauseReseum += PauseReseum;
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player._hitStop += HitStop;
        }
    }
    private void OnDisable()
    {
        _gameManager._pauseReseum -= PauseReseum;
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player._hitStop -= HitStop;
        }
    }

    private void Update()
    {
        if (!_isPause)
        {
            _timer += Time.deltaTime;
        }
        if(_timer >= 2)
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public IEnumerator HitStopCol()
    {
        Animator animator;
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            animator = GetComponentInChildren<Animator>();
        }
        
        animator.speed = 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);
        animator.speed = 1;
    }
    public void HitStop()
    {
        StartCoroutine(HitStopCol());
    }
    public void PauseReseum(bool pause)
    {
        Animator animator;
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (pause)
        {
            _isPause = true;
            animator.speed = 0;
        }
        else
        {
            _isPause = false;
            animator.speed = 1;
        }
    }
}
