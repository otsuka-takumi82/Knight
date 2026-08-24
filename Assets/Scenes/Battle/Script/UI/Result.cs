using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Result : MonoBehaviour
{
    EnemyHelth _eH;
    [SerializeField]
    Text _score;
    [SerializeField]
    Text _name;
    [SerializeField]
    Image _enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eH = FindFirstObjectByType<EnemyHelth>();
        _score.text = _eH._enemyScore.ToString("0");
        _name.text = _eH._name;
        _enemy.sprite = _eH._enemyImage;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
