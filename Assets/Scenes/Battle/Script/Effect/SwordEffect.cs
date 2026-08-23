using Unity.VisualScripting;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    [SerializeField, UnitHeaderInspectable("火花")]
    GameObject _hibana;

    private EnemyHelth _enemyHelth;
    private Player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyHelth = FindFirstObjectByType<EnemyHelth>();
        _player = FindFirstObjectByType<Player>();
    }

    private void OnDestroy()
    {
        Debug.Log("破壊");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RightUp"))
        {
            Debug.Log("a");
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina();
            Hibana(transform.position);
        }
        if (collision.gameObject.CompareTag("Hit2"))
        {
            _enemyHelth.Knock();
            _enemyHelth.PlayerDamage();
            _enemyHelth.PlayerStamina(2);
            Hibana(transform.position);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Hit1"))
        {
            _enemyHelth.Knock();
            _enemyHelth.PlayerStamina();
            _player.ModifyStamina();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            _enemyHelth.Knock();
            _player.ModifyStamina();
            Destroy(collision.gameObject);
        }
    }
    public void Hibana(Vector3 hibanapos)
    {
        Instantiate(_hibana, hibanapos, Quaternion.identity);
    }
}
