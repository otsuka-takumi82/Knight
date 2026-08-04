using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _event;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
