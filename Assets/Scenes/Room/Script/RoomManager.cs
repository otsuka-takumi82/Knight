using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    int _stageNum;
    [SerializeField]
    Text[] _stageName;

    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _gameManager._currentTimeNum = 0;
        AllCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMorning()
    {
        ChangeStage(0);
        
    }
    public void ChangeAfterNoon()
    {
        ChangeStage(1);
    }
    public void ChangeEvening()
    {
        ChangeStage(2);
    }
    public void ChangeStage(int num)
    {
        if(3 > _gameManager._stageNum[num])
        {
            _gameManager._stageNum[num]++;
        }
        else if (_gameManager._stageNum[num] == 3)
        {
            _gameManager._stageNum[num] = 0;
        }
        CheckStage(num);
        
    }

    public void CheckStage(int num)
    {
        if (_gameManager._stageNum[num] == 0)
        {
            _stageName[num].text = "祈り";
        }
        else if (_gameManager._stageNum[num] == 1)
        {
            _stageName[num].text = "戦闘";
        }
        else if (_gameManager._stageNum[num] == 2)
        {
            _stageName[num].text = "鍛冶";
        }
    }

    public void AllCheck()
    {
        CheckStage(0);
        CheckStage(1);
        CheckStage(2);
    }
}
