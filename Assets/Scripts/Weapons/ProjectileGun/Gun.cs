using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData Data;
    public GameObject Bullet;
    public Camera Camera;
    public Transform SpawnPoint;
    private bool reloading = false;
    private bool shotDelay = false;
    private int ammoAmount;

    void Start()
    {
        ammoAmount = Data.AmmoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            StartCoroutine((Reload()));
        }
        if (!reloading && !shotDelay && ammoAmount > 0 && Input.GetButton("Fire1"))
        {
            Fire();
            StartCoroutine(CanShoot());
        }
    }

    private void Fire()
    {
        //Get player angle relative to mouse
        var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        var relativePoint = transform.position - mousePosition;
        var rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;

        //Spawn bullets
        for (int i = 0; i < Data.BulletPerTrigger; i++)
        {
            var eulerAngle = Quaternion.Euler(0, 0, rotation + Random.Range(-Data.Spread, Data.Spread));
            GameObject bullet = Instantiate(Bullet, SpawnPoint.position, eulerAngle);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.up * Data.BulletSpeed;
        }
    }
    
    private IEnumerator Reload()
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(Data.ReloadSpeed);
        Debug.Log("Done");
        ammoAmount = Data.AmmoCapacity;
        reloading = false;
    }

    private IEnumerator CanShoot()
    {
        shotDelay = true;
        ammoAmount -= Data.BulletPerTrigger;
        yield return new WaitForSeconds(Data.BulletPerSecond);
        shotDelay = false;
    }
}
