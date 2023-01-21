using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaptopGoonAI : MonoBehaviour
{
    AIPath aiPath;
    AIDestinationSetter locationSetter;
    public GameObject Player;
    public float followDistance;
    public float dangerDistance;
    public float runAwayDistance;
    Seeker seeker;
    bool runAway = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
        locationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsMovement();
        keepDistance();
    }

    void keepDistance()
    {
        aiPath.isStopped = false;
        float distance = DistanceBetweenPlayer(Player);
        if (runAway && runAwayDistance <= distance)
        {
            runAway = false;
        }
        else if (!runAway && dangerDistance >= distance)
        {
            runAway = true;
        }

        if (runAway)
        {
            seeker.StartPath(transform.position, (2 * transform.position) - Player.transform.position);
        }
        else
        {
            if (distance > followDistance)
            {
                seeker.StartPath(transform.position, Player.transform.position);
            }
            else
            {
                aiPath.isStopped = true;
            }
        }
    }

    float DistanceBetweenPlayer(GameObject player)
    {
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }

    void LookTowardsMovement()
    {
        if (aiPath.isStopped)
        {
            Debug.Log("Stop");
            if (transform.position[0] < Player.transform.position[0])
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (aiPath.desiredVelocity.x >= 0.15f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (aiPath.desiredVelocity.y <= 0.15f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
