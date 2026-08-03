using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
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
}
