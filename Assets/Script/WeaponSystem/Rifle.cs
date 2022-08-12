using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Audio;

public class Rifle : MonoBehaviour
{
    public float RayDistance;

    // Raycaste Muzzle
    public GameObject Muzzle;

    // Projectile Bullet
    public GameObject Bullet;

    // Projectile Velocity
    public float launchVelocity;

    // Rifle Flash
    public GameObject FlashOne;
    public GameObject FlashTwo;
    public GameObject FlashThree;

    bool reloading = false;
    [SerializeField] bool isShooting = false;
    [SerializeField] bool isEmpty = false;
    [SerializeField] bool isReloaded = false;
    [SerializeField] bool isReloading = false;

    public Color OReloaded;

    public TextMeshProUGUI RifleAmmoCount;
    public int RifleAmmo;
    float HalfAmmo;
    float FullAmmo;

    public TextMeshProUGUI RifleTotalAmmo;
    public int RifletotalAmmo;

    // For Crate.cs to check if total ammo is full
    public int RifleFulltotalAmmo;

    [SerializeField] int AmmoFired;

    // Audio
    public AudioClip bang;
    public AudioClip reload;
    public AudioClip shell;
    public AudioSource audioSource;

    public float fireRate = 28f;
    private float timeToFire = 0f;

    float waitToReload = 0f;
    float timeToDelay = 4f;

    private void Start()
    {
        RifleAmmoCount.text = RifleAmmo.ToString();
        RifleTotalAmmo.text = RifletotalAmmo.ToString();

        // Ammo Calculation
        HalfAmmo = RifleAmmo / 2;
        FullAmmo = RifleAmmo;

        // TotalAmmo Check
        RifleFulltotalAmmo = RifletotalAmmo;

        // Get Audio Component
        audioSource = GetComponent<AudioSource>();

    }

    private void LateUpdate()
    {
        StartCoroutine(AutoShoot());

        if(RifleAmmo < FullAmmo)
        {
            waitToReload = waitToReload + 1f * Time.deltaTime;
            if (waitToReload >= timeToDelay)
            {
                waitToReload = 0f;
                OnReload();
            }
        }
    }

    IEnumerator AmmoDeduct()
    {
        if (RifleAmmo == 0)
        {
            isEmpty = true;
            isShooting = false;

            RifleAmmo = 0;
            RifleAmmoCount.text = RifleAmmo.ToString();
        }
        else if (RifleAmmo <= HalfAmmo)
        {
            isEmpty = false;

            RifleAmmoCount.color = Color.red;
            RifleAmmoCount.text = RifleAmmo.ToString();
        }

        isShooting = true;

        FlashOne.SetActive(true);
        FlashTwo.SetActive(true);
        FlashThree.SetActive(true);

        --RifleAmmo;
        ++AmmoFired;
        Debug.Log("Ammo Fired: " + AmmoFired);
        RifleAmmoCount.text = RifleAmmo.ToString();

        yield return new WaitForSeconds(0.05f);
        isShooting = false;

        FlashOne.SetActive(false);
        FlashTwo.SetActive(false);
        FlashThree.SetActive(false);
    }

    IEnumerator AmmoReload()
    {
        if (RifleAmmo == FullAmmo)
        {
            isReloaded = true;
            reloading = false;
            isReloading = false;
            isEmpty = false;

            yield return new WaitForSeconds(1f);
            isReloaded = false;
        }
        else if (reloading && RifleAmmo > 0)
        {
            reloading = false;
        }
        else if (reloading && RifletotalAmmo == 0)
        {
            reloading = false;
        }
        else if (reloading && RifleAmmo < FullAmmo)
        {
            isReloading = true;
            isEmpty = false;

            RifleAmmo = RifleAmmo + AmmoFired;
            RifleAmmoCount.text = RifleAmmo.ToString();

            RifleAmmoCount.color = OReloaded;
            RifleAmmoCount.text = RifleAmmo.ToString();

            //Debug.Log("Ammo Remainding: " + AmmoRemainding);

            RifletotalAmmo = RifletotalAmmo - AmmoFired;
            RifleTotalAmmo.text = RifletotalAmmo.ToString();
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
        yield return new WaitForSeconds(2f);
        Destroy(GameObject.Find("Bullet(Clone)"));
        Destroy(GameObject.Find("Sphere(Clone)"));
    }

    IEnumerator AutoShoot()
    {
        yield return new WaitForSeconds(1f);
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            OnShoot();
        }
    }

    void OnShoot()
    {
        if (RifleAmmo > 0)
        {
            audioSource.PlayOneShot(bang);
            //fire = true;
            StartCoroutine(AmmoDeduct());
            audioSource.PlayOneShot(shell);
            Debug.Log("Shooting");

            GameObject bullet = Instantiate(Bullet, transform.position, Bullet.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchVelocity);
            StartCoroutine(BulletClear());
        }
    }


    void OnReload()
    {
        reloading = true;
        StartCoroutine(AmmoReload());
        Debug.Log("Reloading");
    }
}
