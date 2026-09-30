using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    GameManager _gm;
    [SerializeField]
    UnityEvent[] _event;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = FindFirstObjectByType<GameManager>();
        if (_gm._isSave)
        {
            _event[1].Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartScene()
    {
        StartCoroutine(SceneLoad("RoomScene", 0));
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

    public void SetLoad()
    {
        _gm.SetLoad();
    }
    public void NewGame()
    {
        _gm.NewGame();
    }
}
