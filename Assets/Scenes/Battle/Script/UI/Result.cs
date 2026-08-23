using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    EnemyHelth _eH;
    [SerializeField]
    Text _score;
    [SerializeField]
    Image _enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eH = FindFirstObjectByType<EnemyHelth>();
        _score.text = _eH._enemyScore.ToString("0");
        _enemy.sprite = _eH._enemyImage;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
