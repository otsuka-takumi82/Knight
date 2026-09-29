using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _events;
    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadElseScene(string scenename)
    {
        _gameManager.SetSetting();
        SceneManager.LoadScene(scenename);
    }

    public void LoadTitle()
    {
       LoadElseScene("TitleScene");
    }
    public void LoadRoom()
    {
        _gameManager._currentFight = _gameManager._saveFight;
        LoadElseScene("RoomScene");

    }
    public void LoadTalk()
    {
        LoadElseScene("TalkScene");
    }
    public void LoadPrayer()
    {
        LoadElseScene("PrayerScene");
    }

    public void LoadBattle()
    {
        LoadElseScene("BattleScene");
    }

    public void LoadSwordMake()
    {
        LoadElseScene("MakeScene");
    }
    public void LoadClear()
    {
        LoadElseScene("ClearScene");
    }
    public void LoadGameOver()
    {
        LoadElseScene("GameOverScene");
    }

    public void LoadTimeAdd()
    {
        StartCoroutine(SceneLoad(0));
        
    }

    public void Goat()
    {
        FindFirstObjectByType<RoomManager>()._goat = true;
        StartCoroutine(NoTimeSceneLoad("BattleScene"));
    }
    public IEnumerator SceneLoad(int num)
    {
        //if (_event[num] != null)
        //{
        //    _event[num].Invoke();
        //}
        _gameManager.BrackOut();
        
        _gameManager._currentTimeNum++;
        yield return new WaitForSeconds(1);
        if (_gameManager._currentTimeNum <= 3)
        {
            LoadTalk();
        }
        else
        {
            LoadRoom();
        }
    }
    public IEnumerator NoTimeSceneLoad(string scene)
    {
        _gameManager.BrackOut();
        yield return new WaitForSeconds(1);
        _gameManager._saveFight = _gameManager._currentFight;
        _gameManager._currentFight = 5;
        LoadElseScene(scene);
    }

}
