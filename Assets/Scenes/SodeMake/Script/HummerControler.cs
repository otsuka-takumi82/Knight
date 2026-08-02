using UnityEngine;

public class HummerControler : MonoBehaviour
{
    Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hummer()
    {
        _anim.SetTrigger("Hummer");
    }
}
