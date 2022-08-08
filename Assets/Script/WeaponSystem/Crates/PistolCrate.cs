using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PistolCrate : MonoBehaviour
{
    // Link to the Pistol Raycast object
    private Pistol pistol;

    int RefillPistolTotalAmmo;

    // Time to wait to get Refilled again
    public float timeRefilled;

    // Time for AmmoFull
    public float timeAmmoFull;

    // Count Down UI
    public TextMeshPro CountDown;

    // Full Ammo UI
    public TextMeshPro AmmoFull;

    public void Refill(Pistol newPistol)
    {
        pistol = newPistol;

        StartCoroutine(Refilled());
    }

    IEnumerator Refilled()
    {
        pistol = GameObject.Find("Muzzle").GetComponent<Pistol>();

        // For Pistol Refill
        if (pistol.PistoltotalAmmo == pistol.PistolFulltotalAmmo)
        {
            AmmoFull.gameObject.SetActive(true);

            yield return new WaitForSeconds(timeAmmoFull);

            AmmoFull.gameObject.SetActive(false);
        }
        else if (pistol.PistoltotalAmmo < pistol.PistolFulltotalAmmo)
        {
            CountDown.gameObject.SetActive(true);

            RefillPistolTotalAmmo = 15;
            pistol.PistoltotalAmmo = pistol.PistoltotalAmmo + RefillPistolTotalAmmo;
            pistol.PistolTotalAmmo.text = pistol.PistoltotalAmmo.ToString();

            yield return new WaitForSeconds(timeRefilled);

            CountDown.gameObject.SetActive(false);
        }
    }
}