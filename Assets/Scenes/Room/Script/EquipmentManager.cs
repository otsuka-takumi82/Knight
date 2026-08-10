using UnityEngine;

public class EquipmentManager : MonoBehaviour
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
    public void EquipDefaultSword()
    {
        
        if (_gameManager._wepon[0]._isCrafted)
        {
            _gameManager._currentEquipped = 0;
        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
        }
    }
    public void EquipSword()
    {

        if (_gameManager._wepon[1]._isCrafted)
        {
            _gameManager._currentEquipped = 1;
        }
        else
        {
            Debug.LogWarningFormat("まだ作成していない！");
        }
    }
}
