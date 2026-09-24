using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;



public class Result : MonoBehaviour
{
    EnemyHelth _eH;
    [SerializeField]
    Text _score;
    [SerializeField]
    Text _addScore;
    [SerializeField]
    Text _name;
    [SerializeField]
    Image _enemy;
    [SerializeField]
    AudioClip _bgm;
    private Player _player;
    AudioSource _audio;
    [SerializeField] AudioSource _elseAudio;
    float _addScoreNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _elseAudio.resource = _bgm;
        _audio.PlayOneShot(_bgm);
        _eH = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
        if (_player._commboNum > _player._saveCommbo)
        {
            _player._saveCommbo = _player._commboNum;
        }
        _addScoreNum = _player.AddScore();
        _addScore.text = $@"コンボボーナス+
{_addScoreNum.ToString("0")}";
        _score.text = _eH._enemyScore.ToString("0");
        _name.text = _eH._name;
        _enemy.sprite = _eH._enemyImage;
        
        StartCoroutine(TimeAdd());
        
    }

    public IEnumerator TimeAdd()
    {
        yield return new WaitForSeconds(1);
        while (_addScoreNum > 0)
        {
            _addScoreNum -= Time.deltaTime * 60;
            _addScoreNum = Mathf.Max(0f, _addScoreNum);
            _addScore.text = $@"コンボボーナス+
{_addScoreNum.ToString("0")}";
            _eH._enemyScore += Time.deltaTime * 100;
            _score.text = _eH._enemyScore.ToString("0");
            yield return null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
