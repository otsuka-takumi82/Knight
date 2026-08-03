using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PlayerState
    { 
        Nomal,
        Powor,
        Guald
    }
    [SerializeField, Header("プレイヤー状態")]
    public PlayerState _playerState;

    public int _harb;
    public int _highHarb;
    private void Awake()
    {
        if (FindObjectsByType<GameManager>(
    FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHarb(int num)
    {
        _harb += num;
    }
    public void AddHighHarb(int num)
    {
        _highHarb += num;
    }
    public void ChangeState(PlayerState buff)
    {
        _playerState = buff;
    }
    public bool State(PlayerState state)
    {
        return _playerState == state;
    }
}
