using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        
        Launch();
    }

    // Update is called once per frame
    public void Reset()
    {
        tr.emitting = false;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        speed = 5;
        Launch();
       
    }

    private void Launch()
    {
       float x = Random.Range(0, 2) == 0 ? -1 : 1;
       float y = Random.Range(0, 2) == 0 ? -1 : 1;
       rb.velocity = new Vector2(speed * x, speed * y);
       tr.emitting = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            speed = (float)(speed + 0.25);
            rb.velocity = new Vector2(speed, speed);
        }
    }
}
