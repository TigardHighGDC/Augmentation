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
    private Vector2 NormalizeSum(float x, float y)
    {
        float combine = Mathf.Abs(x) + Mathf.Abs(y); 
        if (combine < 0.001f)
        {
            return new Vector2(0f, 0f);
        }
        else
        {
            return new Vector2(x/combine, y/combine); 
        }
        
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called with frequency of physics engine
    void FixedUpdate()
    {
        // Set velocity to current input
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        //rb.velocity = NormalizeSum(x, y) * speed;
        direction = new Vector2(x, y);
        //Debug.Log(direction);
        direction.Normalize();
        Debug.Log(direction);
        rb.velocity = direction * speed;
    }
}
