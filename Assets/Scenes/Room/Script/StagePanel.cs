using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StagePanel : MonoBehaviour
{
    [SerializeField] int _num;
    Image _image;
    GameManager _gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
        _gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.sprite = _gm._stageImage[_gm._stageNum[_num]];
    }
}
