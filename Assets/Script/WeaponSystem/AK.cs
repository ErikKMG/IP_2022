using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AK : MonoBehaviour
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

    public TextMeshProUGUI AKAmmoCount;
    public int AKAmmo;
    float HalfAmmo;
    float FullAmmo;

    public TextMeshProUGUI AKTotalAmmo;
    public int AKtotalAmmo;

    // For Crate.cs to check if total ammo is full
    public int AKFulltotalAmmo;

    [SerializeField] int AmmoFired;

    private void Start()
    {
        AKAmmoCount.text = AKAmmo.ToString();
        AKTotalAmmo.text = AKtotalAmmo.ToString();

        // Ammo Calculation
        HalfAmmo = AKAmmo / 2;
        FullAmmo = AKAmmo;

        // TotalAmmo Check
        AKFulltotalAmmo = AKtotalAmmo;
    }

    IEnumerator AmmoDeduct()
    {
        if (AKAmmo == 0)
        {
            isEmpty = true;
            isShooting = false;

            AKAmmo = 0;
            AKAmmoCount.text = AKAmmo.ToString();
        }
        else if (AKAmmo <= HalfAmmo)
        {
            isEmpty = false;

            AKAmmoCount.color = Color.red;
            AKAmmoCount.text = AKAmmo.ToString();
        }

        isShooting = true;

        --AKAmmo;
        ++AmmoFired;
        Debug.Log("Ammo Fired: " + AmmoFired);
        AKAmmoCount.text = AKAmmo.ToString();

        yield return new WaitForSeconds(1f);
        isShooting = false;

    }

    IEnumerator AmmoReload()
    {
        if (AKAmmo == FullAmmo)
        {
            isReloaded = true;
            reloading = false;
            isReloading = false;
            isEmpty = false;

            yield return new WaitForSeconds(1f);
            isReloaded = false;
        }
        else if (reloading && AKAmmo > 0)
        {
            reloading = false;
        }
        else if (reloading && AKtotalAmmo == 0)
        {
            reloading = false;
        }
        else if (reloading && AKAmmo < FullAmmo)
        {
            isReloading = true;
            isEmpty = false;

            AKAmmo = AKAmmo + AmmoFired;
            AKAmmoCount.text = AKAmmo.ToString();

            AKAmmoCount.color = OReloaded;
            AKAmmoCount.text = AKAmmo.ToString();

            //Debug.Log("Ammo Remainding: " + AmmoRemainding);

            AKtotalAmmo = AKtotalAmmo - AmmoFired;
            AKTotalAmmo.text = AKtotalAmmo.ToString();
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
        if (AKAmmo > 0)
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
