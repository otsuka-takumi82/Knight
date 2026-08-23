using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    Text _score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("a");
        _score.text =  FindFirstObjectByType<EnemyHelth>()._enemyScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
