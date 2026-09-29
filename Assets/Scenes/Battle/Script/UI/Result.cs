using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;



public class Result : MonoBehaviour
{
    [SerializeField]
    GameObject[] _button;
    EnemyHelth _eH;
    [SerializeField]
    Text _score;
    [SerializeField]
    Text _addScore;
    [SerializeField]
    Text _name;
    [SerializeField,Header("金")] Text _moneyText;
    [SerializeField]
    Image _enemy;
    [SerializeField]
    AudioClip _bgm;
    private Player _player;
    AudioSource _audio;
    [SerializeField] AudioSource _elseAudio;
    float _addScoreNum;
    int _money = 0;
    GameManager _gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm =  FindFirstObjectByType<GameManager>();
        _moneyText.text = @$":合計金額
{_gm._money.ToString()}";
        _audio = GetComponent<AudioSource>();
        _elseAudio.resource = _bgm;
        _audio.PlayOneShot(_bgm);
        _eH = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
        if(_eH._name == "Goat")
        {
            _button[0].SetActive(false);
        }
        else
        {
            _button[1].SetActive(false);
        }
        if (_player._commboNum > _player._saveCommbo)
        {
            _player._saveCommbo = _player._commboNum;
        }
        _addScoreNum = _player.AddScore();
        _addScore.text = $@"コンボボーナス+
{_player._saveCommbo .ToString("0")}×{_player._addScore.ToString("0")}";
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
            _addScoreNum -= Time.deltaTime * 100;
            _addScoreNum = Mathf.Max(0f, _addScoreNum);
            _addScore.text = $@"コンボボーナス+
{_addScoreNum.ToString("0")}";
            _eH._enemyScore += Time.deltaTime * 100;
            _money = Mathf.FloorToInt(_eH._enemyScore);
            _score.text = _money.ToString("0");
            yield return null;
        }
        _gm._money += _money;
        _moneyText.text = @$":合計金額
{_gm._money.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
