using System.Collections;
using UnityEngine;

public class HibanaSodeMake : MonoBehaviour
{
    Player _player;
    private void Awake()
    {
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player = FindFirstObjectByType<Player>();
        }
    }
    private void OnEnable()
    {
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player._hitStop += HitStop;
        }
    }
    private void OnDisable()
    {
        if (FindFirstObjectByType<EnemyHelth>() != null)
        {
            _player._hitStop -= HitStop;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
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
}
