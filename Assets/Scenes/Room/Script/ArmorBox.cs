using UnityEngine;

public class ArmorBox : WeponBox
{


    // Update is called once per frame
    public override void Update()
    {
        if(_gameManager._isArmored)
        {
            _wepon.sprite = _gameManager._armorImage[1];
        }
        else
        {
            _wepon.sprite = _gameManager._armorImage[0];
        }
        
    }
}
