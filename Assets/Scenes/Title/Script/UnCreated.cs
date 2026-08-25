using UnityEngine;

public class UnCreated : MonoBehaviour
{
    [SerializeField]
    float _destroyTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
