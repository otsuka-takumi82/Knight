using UnityEngine;
using UnityEngine.Events;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] _events;
    [SerializeField]Transform _target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindFirstObjectByType<Player>()._stagging)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _events[0].Invoke();
                _target.position = new Vector3(0, 0, 0);
            }
        }
        
    }
}
