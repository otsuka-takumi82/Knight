using UnityEngine;
using UnityEngine.UI;

public class PrayUIManager : MonoBehaviour
{
    [SerializeField]
    Image _gage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GageControl(float gage, float maxFloat)
    {
        _gage.fillAmount = (float)gage / maxFloat;
    }
}
