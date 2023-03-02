using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected GameObject player;
    protected AIPhysics aiPath;
    protected Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        aiPath = GetComponent<AIPhysics>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected float DistanceBetweenPlayer()
    {
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }
}
