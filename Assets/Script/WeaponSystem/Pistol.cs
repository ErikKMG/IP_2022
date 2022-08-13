using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Pistol : MonoBehaviour
{
    public float RayDistance;

    // Raycaste Muzzle
    public GameObject Muzzle;

    // Projectile Bullet
    public GameObject Bullet;

    // Projectile Velocity
    public float launchVelocity;

    // Pistol Flash
    public GameObject FlashOne;
    public GameObject FlashTwo;
    public GameObject FlashThree;

    bool reloading = false;
    [SerializeField] bool isShooting = false;
    [SerializeField] bool isEmpty = false;
    [SerializeField] bool isReloaded = false;
    [SerializeField] bool isReloading = false;

    public Color OReloaded;

    public TextMeshProUGUI PistolAmmoCount;
    public int PistolAmmo;
    float HalfAmmo;
    float FullAmmo;

    public TextMeshProUGUI PistolTotalAmmo;
    public int PistoltotalAmmo;

    // For Crate.cs to check if total ammo is full
    public int PistolFulltotalAmmo;

    [SerializeField] int AmmoFired;

    // Audio
    public AudioClip bang;
    public AudioClip reload;
    public AudioClip shell;
    public AudioSource audioSource;

    private void Start()
    {
        PistolAmmoCount.text = PistolAmmo.ToString();
        PistolTotalAmmo.text = PistoltotalAmmo.ToString();

        // Ammo Calculation
        HalfAmmo = PistolAmmo / 2;
        FullAmmo = PistolAmmo;

        // TotalAmmo Check
        PistolFulltotalAmmo = PistoltotalAmmo;

        //Get Audio Component
        audioSource = GetComponent<AudioSource>();
    }

    //private void Update()
    //{
    //    ShootRay();
    //}

    //public void ShootRay()
    //{
    //    RaycastHit ShootInfo;

    //    Debug.DrawLine(Muzzle.transform.position, Muzzle.transform.position + (Muzzle.transform.forward * RayDistance));

    //    if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out ShootInfo, RayDistance))
    //    {
    //        //Debug.Log(ShootInfo.transform.name);

    //        if (ShootInfo.transform)
    //        {
    //            if (fire)
    //            {
    //                //Destroy(ShootInfo.transform.gameObject);
    //                //Debug.Log("Target Destory");
                    
    //            }
    //        }
    //    }
    //    fire = false;
    //}

    IEnumerator AmmoDeduct()
    {
        if (PistolAmmo == 0)
        {
            isEmpty = true;
            isShooting = false;

            PistolAmmo = 0;
            PistolAmmoCount.text = PistolAmmo.ToString();
        }
        else if (PistolAmmo <= HalfAmmo)
        {
            isEmpty = false;

            PistolAmmoCount.color = Color.red;
            PistolAmmoCount.text = PistolAmmo.ToString();
        }

        isShooting = true;

        FlashOne.SetActive(true);
        FlashTwo.SetActive(true);
        FlashThree.SetActive(true);

        --PistolAmmo;
        ++AmmoFired;
        Debug.Log("Ammo Fired: " + AmmoFired);
        PistolAmmoCount.text = PistolAmmo.ToString();

        yield return new WaitForSeconds(0.05f);
        isShooting = false;

        FlashOne.SetActive(false);
        FlashTwo.SetActive(false);
        FlashThree.SetActive(false);
    }

    IEnumerator AmmoReload()
    {
        if (PistolAmmo == FullAmmo)
        {
            isReloaded = true;
            reloading = false;
            isReloading = false;
            isEmpty = false;

            yield return new WaitForSeconds(1f);
            isReloaded = false;
        }
        else if (reloading && PistolAmmo > 0)
        {
            reloading = false;
        }
        else if (reloading && PistoltotalAmmo == 0)
        {
            reloading = false;
        }
        else if (reloading && PistolAmmo < FullAmmo)
        {
            isReloading = true;
            isEmpty = false;

            PistolAmmo = PistolAmmo + AmmoFired;
            PistolAmmoCount.text = PistolAmmo.ToString();

            PistolAmmoCount.color = OReloaded;
            PistolAmmoCount.text = PistolAmmo.ToString();

            //Debug.Log("Ammo Remainding: " + AmmoRemainding);

            PistoltotalAmmo = PistoltotalAmmo - AmmoFired;
            PistolTotalAmmo.text = PistoltotalAmmo.ToString();
            AmmoFired = 0;

            audioSource.PlayOneShot(reload);
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
        if (PistolAmmo > 0)
        {
            audioSource.PlayOneShot(bang);
            StartCoroutine(AmmoDeduct());
            audioSource.PlayOneShot(shell);
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