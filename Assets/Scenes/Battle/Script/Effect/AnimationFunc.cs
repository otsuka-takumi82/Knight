using UnityEngine;
using UnityEngine.Events;

public class AnimationFunc : MonoBehaviour
{
    [SerializeField] UnityEvent[] _events;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSwordEffect()
    {
        GetComponentInChildren<SwordEffect>().GetCombo();
    }
    public void Audio()
    {
        _events[0].Invoke();
        //スキルの音
    }
}
