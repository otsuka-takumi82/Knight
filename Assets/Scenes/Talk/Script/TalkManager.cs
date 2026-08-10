using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//StartとUpdateのなかにsummaryの指示に従って入れればいい
public class TalkManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _event;
    [SerializeField,Header("シスターのゲームオブジェクト")]
    GameObject _sister;
    [SerializeField, Header("ファイターのゲームオブジェクト")]
    GameObject _fighter;
    [SerializeField, Header("鍛冶屋のゲームオブジェクト")]
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
    [SerializeField]
    GameObject _makeWepon;

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
    #region 武器のボタンの数字戻り値
    public void NumDefaultSword()
    {
        _gameManager._currentMake = 0;
    }
    public void NumSword()
    {
        _gameManager._currentMake = 1;
    }
    public void NumMace()
    {
        _gameManager._currentMake = 2;
    }
    #endregion
    public void OnWeponSelect()
    {
        if (_makeWepon.activeSelf)
        {
            _makeWepon.SetActive(false);
        }
        else
        {
            _makeWepon.SetActive(true);
        }
    }
    ///<summary>
    ///第一:NPCのゲームオブジェクト
    ///第二:NPCのメッセージ配列
    ///第三:ボタンの配列(0飛ばし)
    ///</summary>
    public void TalkStart(GameObject character, string[] messageType,int num)
    {
        _button[num].SetActive(true);
        character.SetActive(true);
        _diaText.text = messageType[_diaIndex];
    }
    ///<summary>
    ///第一:NPCのメッセージ配列入れればいいだけ
    ///</summary>
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
    #region 次シーンボタンにアタッチするメソッド
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
    #endregion
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
