using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class M4 : MonoBehaviour
{
    public float RayDistance;

    // Raycaste Muzzle
    public GameObject Muzzle;

    // Projectile Bullet
    public GameObject Bullet;

    // Projectile Velocity
    public float launchVelocity;

    bool reloading = false;
    [SerializeField] bool isShooting = false;
    [SerializeField] bool isEmpty = false;
    [SerializeField] bool isReloaded = false;
    [SerializeField] bool isReloading = false;

    public Color OReloaded;

    public TextMeshProUGUI M4AmmoCount;
    public int M4Ammo;
    float HalfAmmo;
    float FullAmmo;

    public TextMeshProUGUI M4TotalAmmo;
    public int M4totalAmmo;

    // For Crate.cs to check if total ammo is full
    public int M4FulltotalAmmo;

    [SerializeField] int AmmoFired;

    private void Start()
    {
        M4AmmoCount.text = M4Ammo.ToString();
        M4TotalAmmo.text = M4totalAmmo.ToString();

        // Ammo Calculation
        HalfAmmo = M4Ammo / 2;
        FullAmmo = M4Ammo;

        // TotalAmmo Check
        M4FulltotalAmmo = M4totalAmmo;
    }

    IEnumerator AmmoDeduct()
    {
        if (M4Ammo == 0)
        {
            isEmpty = true;
            isShooting = false;

            M4Ammo = 0;
            M4AmmoCount.text = M4Ammo.ToString();
        }
        else if (M4Ammo <= HalfAmmo)
        {
            isEmpty = false;

            M4AmmoCount.color = Color.red;
            M4AmmoCount.text = M4Ammo.ToString();
        }

        isShooting = true;

        --M4Ammo;
        ++AmmoFired;
        Debug.Log("Ammo Fired: " + AmmoFired);
        M4AmmoCount.text = M4Ammo.ToString();

        yield return new WaitForSeconds(1f);
        isShooting = false;

    }

    IEnumerator AmmoReload()
    {
        if (M4Ammo == FullAmmo)
        {
            isReloaded = true;
            reloading = false;
            isReloading = false;
            isEmpty = false;

            yield return new WaitForSeconds(1f);
            isReloaded = false;
        }
        else if (reloading && M4Ammo > 0)
        {
            reloading = false;
        }
        else if (reloading && M4totalAmmo == 0)
        {
            reloading = false;
        }
        else if (reloading && M4Ammo < FullAmmo)
        {
            isReloading = true;
            isEmpty = false;

            M4Ammo = M4Ammo + AmmoFired;
            M4AmmoCount.text = M4Ammo.ToString();

            M4AmmoCount.color = OReloaded;
            M4AmmoCount.text = M4Ammo.ToString();

            //Debug.Log("Ammo Remainding: " + AmmoRemainding);

            M4totalAmmo = M4totalAmmo - AmmoFired;
            M4TotalAmmo.text = M4totalAmmo.ToString();
            AmmoFired = 0;

            yield return new WaitForSeconds(3f);
            //AmmoRemainding = 0;
            isReloading = false;
        }
        reloading = false;
    }

    IEnumerator BulletClear()
    {
        yield return new WaitForSeconds(15f);
        Destroy(GameObject.Find("Bullet(Clone)"));
        Destroy(GameObject.Find("Sphere(Clone)"));
    }

    void OnShoot(InputValue shootValue)
    {
        if (M4Ammo > 0)
        {
            //fire = true;
            StartCoroutine(AmmoDeduct());
            Debug.Log("Shooting");

            GameObject bullet = Instantiate(Bullet, transform.position, Bullet.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchVelocity);
            StartCoroutine(BulletClear());
        }
    }

    void OnReload(InputValue reloadValue)
    {
        reloading = true;
        StartCoroutine(AmmoReload());
        Debug.Log("Reloading");
    }
}
