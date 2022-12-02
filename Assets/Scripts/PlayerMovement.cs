using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float x;
    private float y;
    private Vector2 direction;
    public float speed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called with frequency of physics engine
    void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y);

        direction.Normalize();
        rb.velocity = direction * speed;
    }
}
