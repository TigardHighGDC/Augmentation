using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData Data;
    public GameObject Bullet;
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
        if(!reloading && !shotDelay && ammoAmount > 0 && Input.GetButton("Fire1"))
        {
            StartCoroutine(CanShoot());
            Debug.Log(ammoAmount);
        }  
    }

    private void Fire()
    {
        var look = Vector3.Angle(transform.position, Input.mousePosition);
        GameObject bullet = Instantiate(Bullet, transform.position, look);
    }

    IEnumerator Reload() 
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(Data.ReloadSpeed);
        ammoAmount = Data.AmmoCapacity;
        reloading = false;
    }

    IEnumerator CanShoot() 
    {
        shotDelay = true;
        ammoAmount -= Data.BulletPerTrigger;
        yield return new WaitForSeconds(Data.BulletPerSecond);
        shotDelay = false;
    }
}
