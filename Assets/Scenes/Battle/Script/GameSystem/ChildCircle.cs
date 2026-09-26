using System.Collections;
using UnityEngine;

public class ChildCircle : MonoBehaviour
{
    GameObject _parent;
    Vector2 _save;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _parent = transform.parent.gameObject;
        StartCoroutine(Diley());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer _sprite;
        _sprite = _parent.GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag.StartsWith("Hit"))
        {

            Rigidbody2D pRb = _parent.GetComponent<Rigidbody2D>();
            _save = pRb.linearVelocity;
            pRb.linearVelocity *= 0.5f;
            _sprite.color = Color.green;
            _parent.gameObject.tag = "GoodBall";
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        SpriteRenderer _sprite;
        _sprite = _parent.GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag.StartsWith("Hit"))
        {
            Rigidbody2D pRb = _parent.GetComponent<Rigidbody2D>();
            pRb.linearVelocity = _save;
            _sprite.color = Color.yellow;
            _parent.gameObject.tag = "Ball";
        }
    }

    public IEnumerator Diley()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
