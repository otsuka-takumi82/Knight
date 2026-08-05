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
        SceneManager.LoadScene(scenename);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void LoadRoom()
    {
        SceneManager.LoadScene("RoomScene");
    }
    public void LoadTalk()
    {
        SceneManager.LoadScene("TalkScene");
    }
    public void LoadPrayer()
    {
        SceneManager.LoadScene("PrayerScene");
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void LoadSwordMake()
    {
        SceneManager.LoadScene("MakeScene");
    }
    public void LoadClear()
    {
        SceneManager.LoadScene("ClearScene");
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
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
