using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPorchTitle : MonoBehaviour
{
    [SerializeField]
    int _porchNum;

    Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        _image = GetComponent<Image>();
        if( gm._item != null )
        {
            if (gm._item[_porchNum] == GameManager.Item.None)
            {
                _image.sprite = gm._itemImage[0];
            }
            else if (gm._item[_porchNum] == GameManager.Item.Harb)
            {
                _image.sprite = gm._itemImage[1];
            }
            else if (gm._item[_porchNum] == GameManager.Item.HighHarb)
            {
                _image.sprite = gm._itemImage[2];
            }
            else if (gm._item[_porchNum] == GameManager.Item.Meat)
            {
                _image.sprite = gm._itemImage[3];
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
