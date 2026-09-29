using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField, Header("Event")] UnityEvent[] _events;
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
    public void EquipDefaultSword()
    {
        _events[0].Invoke();
        if (_gameManager._wepon[0]._isCrafted)
        {
            _gameManager._currentEquipped = 0;
            _gameManager.EquipUI();
        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
            _gameManager.UnCreated();
        }
    }
    public void EquipSword()
    {
        _events[0].Invoke();
        if (_gameManager._wepon[1]._isCrafted)
        {
            _gameManager._currentEquipped = 1;
            _gameManager .EquipUI();
        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
            _gameManager.UnCreated();
        }
    }
    public void EquipMeis()
    {
        _events[0].Invoke();
        if (_gameManager._wepon[2]._isCrafted)
        {
            _gameManager._currentEquipped = 2;
            _gameManager .EquipUI();
        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
            _gameManager.UnCreated();
        }
    }
    public void EquipArmor()
    {
        _events[0].Invoke();
        if (_gameManager._killEnemy[3] == true)
        {
            if(_gameManager._isArmored)
            {
                _gameManager._isArmored = false;
                _gameManager.EquipUI();
            }
            else
            {
                _gameManager._isArmored = true;
                _gameManager.EquipUI();
            }

        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
            _gameManager.UnCreated();
        }
    }
}
