using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
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
}
