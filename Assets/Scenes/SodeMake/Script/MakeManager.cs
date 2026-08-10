using UnityEngine;

public class MakeManager : MonoBehaviour
{
    [SerializeField]
    GameObject _makeWepon;
    [SerializeField]
    GameObject _weponOBJ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWeponSelect()
    {
        if (_makeWepon.activeSelf)
        {
            _makeWepon.SetActive(false);
        }
        else
        {
            _makeWepon.SetActive(true);
        }
    }
}
