using UnityEngine;
using UnityEngine.UI;

public class WeponPanel : MonoBehaviour
{
    [SerializeField]
    int _weponNum;
    [SerializeField]
    Text _weponPower;
    [SerializeField]
    Text _weonRepair;
    float _savePower;

    string[] _repairStr =
    {
        "故障",
        "通常",
        "完璧"
    };
    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _savePower = _gameManager._wepon[_weponNum]._weponPower;
        if (_gameManager._wepon[_weponNum]._isCrafted)
        {
            if (_gameManager._wepon[_weponNum]._repairPal == 0)
            {
                _savePower *= 0.5f;
            }
            else if (_gameManager._wepon[_weponNum]._repairPal == 1)
            {
                _savePower *= 1;
            }
            else if (_gameManager._wepon[_weponNum]._repairPal == 2)
            {
                _savePower *= 1.5f;
            }
        }
        else
        {
            _savePower *= 1;
        }
        CheckWepon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckWepon()
    {
        _weponPower.text = "攻撃力: " + Mathf.Abs(_savePower).ToString("0.0") ;
        if (_gameManager._wepon[_weponNum]._isCrafted)
        {
            if (_gameManager._wepon[_weponNum]._repairPal == 0)
            {
                _weonRepair.text = "耐久度: " + _repairStr[0];
            }
            else if (_gameManager._wepon[_weponNum]._repairPal == 1)
            {
                _weonRepair.text = "耐久度: " + _repairStr[1];
            }
            else if (_gameManager._wepon[_weponNum]._repairPal == 2)
            {
                _weonRepair.text = "耐久度: " + _repairStr[2];
            }
        }
        else
        {
            _weonRepair.text = "未作成";
        }
        
    }
}
