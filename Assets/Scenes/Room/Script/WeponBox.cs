using UnityEngine;
using UnityEngine.UI;

public class WeponBox : MonoBehaviour
{
    

    Image _wepon;
    GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _wepon = GetComponent<Image>();
        _gameManager = FindFirstObjectByType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _wepon.sprite = _gameManager._swordImage[_gameManager._currentEquipped];
    }
}
