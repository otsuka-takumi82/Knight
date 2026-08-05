using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TalkManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _event;
    [SerializeField]
    GameObject _sister;
    [SerializeField]
    GameObject _fighter;
    [SerializeField]
    GameObject _maker;
    [SerializeField]
    GameObject[] _button;
    [SerializeField]
    Text _diaText;
    [SerializeField]
    private string[] _sisterMessage;
    [SerializeField]
    private string[] _fighterMessage;
    [SerializeField]
    private string[] _makerMessage;

    enum JobType
    {
        Prayer,
        Fighter,
        Maker
    }

    [SerializeField]JobType _jobType;
    private int _diaIndex;

    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        if (_gameManager._stageNum[_gameManager._currentTimeNum] == 1)
        {
            TalkStart(_sister, _sisterMessage,1);

        }
        else if(_gameManager._stageNum[_gameManager._currentTimeNum] == 2)
        {
            TalkStart(_fighter, _fighterMessage,2);
        }
        else if (_gameManager._stageNum[_gameManager._currentTimeNum] == 3)
        {
            TalkStart(_maker, _makerMessage,3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_gameManager._stageNum[_gameManager._currentTimeNum] == 1)
            {
                AddTalk(_sisterMessage);
            }
            else if (_gameManager._stageNum[_gameManager._currentTimeNum] == 2)
            {
                AddTalk(_fighterMessage);
            }
            else if (_gameManager._stageNum[_gameManager._currentTimeNum] == 3)
            {
                AddTalk(_makerMessage);
            }

        }
    }
    //public void PrayTalkStart()
    //{
    //    _sister.SetActive(true);
    //    _diaText.text = _sisterMessage[_diaIndex];
    //}
    public void TalkStart(GameObject character, string[] messageType,int num)
    {
        _button[num].SetActive(true);
        character.SetActive(true);
        _diaText.text = messageType[_diaIndex];
    }

    public void AddTalk(string[] messageType)
    {
        if (messageType.Length <= _diaIndex + 1)
        {
            _diaText.gameObject.SetActive(false);
        }
        if (_diaText.gameObject.activeSelf)
        {
            _diaIndex++;
            _diaText.text = messageType[_diaIndex];
        }
        

    }
    public void PrayScene()
    {
        NextScene("PrayerScene", 0);
    }
    public void BattleScene()
    {
        NextScene("BattleScene", 0);
    }
    public void SodeMakeScene()
    {
        NextScene("MakeScene", 0);
    }

    public void NextScene(string scenename, int num)
    {
        StartCoroutine(SceneLoad(scenename, num));
    }

    public IEnumerator SceneLoad(string scenename, int num)
    {
        if (_event[num] != null)
        {
            _event[num].Invoke();
        }
        yield return new WaitForSeconds(1);
        FindFirstObjectByType<SceneLoader>().LoadElseScene(scenename);
    }
}
