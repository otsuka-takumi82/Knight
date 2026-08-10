using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    int _stageNum;
    [SerializeField]
    Text[] _stageName;
    [SerializeField]
    Text _dayText;
    [SerializeField]
    GameObject _stageSelect;
    [SerializeField]
    GameObject _itemSelect;
    [SerializeField]
    GameObject _equipmentSelect;
    [SerializeField]
    GameObject[] _equipment;

    GameManager _gameManager;
    public bool _isNight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _gameManager.ChangeState(GameManager.PlayerState.Nomal);
        DayUI();
        if (_gameManager._currentTimeNum == 5)
        {
            _gameManager._currentTimeNum = 0;
        }
        if (_gameManager._currentTimeNum == 4)
        {
            _isNight = true;
        }
        else
        {
            _isNight= false;
        }
        AllCheck();
    }
    private void OnDestroy()
    {
        if(_isNight)
        {
            _gameManager._currentDayNum++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStageSelect()
    {
        if(_stageSelect.activeSelf)
        {
            _stageSelect.SetActive(false);
        }
        else
        {
            _stageSelect.SetActive(true);
        }
    }
    public void OnItemSelect()
    {
        if (_itemSelect.activeSelf)
        {
            _itemSelect.SetActive(false);
        }
        else
        {
            _itemSelect.SetActive(true);
        }
    }
    public void OnEquipmentSelect()
    {
        if (_equipmentSelect.activeSelf)
        {
            _equipmentSelect.SetActive(false);
        }
        else
        {
            _equipmentSelect.SetActive(true);
        }
    }
    public void OnEquipmentSword()
    {
        if (_equipment[0].activeSelf)
        {
            _equipment[0].SetActive(false);
        }
        else
        {
            _equipment[0].SetActive(true);
        }
    }
    public void ChangeMorning()
    {
        ChangeStage(1);
        
    }
    public void ChangeAfterNoon()
    {
        ChangeStage(2);
    }
    public void ChangeEvening()
    {
        ChangeStage(3);
    }
    public void ChangeStage(int num)
    {
        
        _gameManager._stageNum[num]++;
        if (_gameManager._stageNum[num] == 4)
        {
            _gameManager._stageNum[num] = 1;
        }
      
        CheckStage(num);
        
    }

    public void CheckStage(int num)
    {
        if (_gameManager._stageNum[num] == 1)
        {
            _stageName[num].text = "祈り";
        }
        else if (_gameManager._stageNum[num] == 2)
        {
            _stageName[num].text = "戦闘";
        }
        else if (_gameManager._stageNum[num] == 3)
        {
            _stageName[num].text = "鍛冶";
        }
    }

    public void AllCheck()
    {
        CheckStage(1);
        CheckStage(2);
        CheckStage(3);
    }
    public void DayUI()
    {
        _dayText.text = "日数：" + _gameManager._currentDayNum;
    }
}
