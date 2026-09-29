using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieroObj : MonoBehaviour,IPointerDownHandler
{
    bool _paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Animator anim = GetComponentInParent<Animator>();
        AudioSource audio = GetComponent<AudioSource>();
        if(_paused)
        {
            anim.speed = 1;
            audio.Play();
            _paused = false;
        }
        else
        {
            anim.speed = 0;
            audio.Stop();
            _paused = true;
        }
    }
}
