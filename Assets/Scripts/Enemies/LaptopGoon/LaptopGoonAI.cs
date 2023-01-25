using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaptopGoonAI : BasicEnemy
{
    AIPath aiPath;
    AIDestinationSetter locationSetter;
    public float followDistance;
    public float dangerDistance;
    public float runAwayDistance;
    Seeker seeker;
    bool runAway = false;
    public float BulletSpeed;
    public GameObject Bullet;
    public float BulletPerSecond;
    bool canFire = true;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
    }

    void Update()
    {
        if (!runAway && canFire)
        {
            StartCoroutine(Fire());
        }
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
            // Goes opposite direction of player
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
        // Calculates the distance between the player and enemy
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }

    void LookTowardsMovement()
    {
        // Look towards the player if stopped and if not look towards the direction it is following.
        if (aiPath.isStopped)
        {
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

    private IEnumerator Fire()
    {
        canFire = false;
        // Get angle to fire at player and convert to euler
        Vector3 relativePoint = transform.position - Player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation);

        // Spawn bullet and provide needed values
        GameObject bullet = Instantiate(Bullet, transform.position, eulerAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<LaptopGoonBullet>().Damage = Damage;
        rb.velocity = bullet.transform.up * BulletSpeed;

        yield return new WaitForSeconds(BulletPerSecond);
        canFire = true;
    }
}
