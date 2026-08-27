using System.Drawing;
using System.Xml.Linq;
using UnityEngine;

public class HItBigCircle : HitCircle
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        _collider.enabled = true;
    }


    public override void TagChange1()
    {
        
    }
    public override void TagChange2()
    {
        
    }
    public override void TimeOver()
    {
        _player._currentHp /= 2;
        _player.ShowHP();
    }   
}
