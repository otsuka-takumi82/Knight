using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RoomManager : MonoBehaviour
{
    [SerializeField, Header("Event")] UnityEvent[] _events;
    [SerializeField]
    int _stageNum;
    [SerializeField]
    Text[] _stageName;
    [SerializeField]
    Text _dayText;
    [SerializeField,Header("Sister")]GameObject _sister;
    [SerializeField,Header("GoatButton")]GameObject _goatButton;
    [SerializeField,Header("報酬たち")]GameObject[] _killObj;
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
    public bool _goat;
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
        if(_isNight )
        {
            if(_gameManager._noPrayDay >= 3)
            {
                _sister.SetActive(true);
            }
            
            _goatButton.SetActive(true);
        }
        AllCheck();
        if (_gameManager._killEnemy[3] == true && !_gameManager._isArmored)
        {
            _killObj[3].SetActive(true);
        }
        else
        {
            _killObj[3].SetActive(false);
        }
        if (_gameManager._killEnemy[4] == true )
        {
            if(!_killObj[4].activeSelf)
            {
                _killObj[4].SetActive(true);
            }
            
        }
        if (_gameManager._killEnemy[5] == true)
        {
            if(!_killObj[5].activeSelf)
            {
                _killObj[5].SetActive(true);
            }
        }
    }
    private void OnDestroy()
    {
        if(_isNight)
        {
            List<int> noDay = _gameManager._stageNum.Where((x, index) => index != 0 && index != 4).ToList();
            if (noDay.All(x => x != 1))
            {
                _gameManager._noPrayDay++;
            }
            if(!_goat)
            {
                Debug.Log("Nomal");
                _gameManager._currentDayNum++;
            }
            _goat = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._killEnemy[3] == true && !_gameManager._isArmored)
        {
            _killObj[3].SetActive(true);
        }
        else
        {
            _killObj[3].SetActive(false);
        }
    }
    public void OnStageSelect()
    {
        _events[0].Invoke();
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
        _events[0].Invoke();
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
        _events[0].Invoke();
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
        _events[0].Invoke();
        if (_equipment[0].activeSelf)
        {
            _equipment[0].SetActive(false);
        }
        else
        {
            _equipment[0].SetActive(true);
        }
    }
    public void OnEquipmentArmored()
    {
        _events[0].Invoke();
        if (_equipment[1].activeSelf)
        {
            _equipment[1].SetActive(false);
        }
        else
        {
            _equipment[1].SetActive(true);
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
        _events[0].Invoke();
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
