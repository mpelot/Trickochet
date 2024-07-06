using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully : MonoBehaviour
{

    public GameObject ball;
    private Rigidbody2D rb;
    public float speed = 0;
    private Vector3 v = Vector3.zero;
    public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = (ball.transform.position - transform.position).normalized;
        Vector3 targetVelocity = direction * speed;

        if (canMove)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref v, .1f);
        }
    }
}
