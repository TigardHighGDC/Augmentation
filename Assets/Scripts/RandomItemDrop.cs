using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : MonoBehaviour
{
    // you will probably want to move things around I just have it how I learned / how I stole it from a different project

    
    public List<GameObject> spawnPool;
    
    private NonPlayerHealth PotHealth;

    void Start()
    {
        PotHealth = GetComponent<NonPlayerHealth>();
    }

    void Update()
    {
        //health to make it "break"
        if(PotHealth.Health <= 0)
        {
            GameObject toSpawn;
            int randomItem = 0;
            

            //random item from the list
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];



            Instantiate(toSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Damage(float damageAmount)
    {
        PotHealth.Health = PotHealth.Health - damageAmount;
    }
}
