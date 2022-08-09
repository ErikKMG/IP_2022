using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RifleCrate : MonoBehaviour
{
    // Link to the Pistol Raycast object
    private Rifle rifle;

    int RefillRifleTotalAmmo;

    // Time to wait to get Refilled again
    public float timeRefilled;

    // Time for AmmoFull
    public float timeAmmoFull;

    // Count Down UI
    public TextMeshPro CountDown;

    // Full Ammo UI
    public TextMeshPro AmmoFull;

    public void Refill(Rifle newRifle)
    {
        rifle = newRifle;

        StartCoroutine(Refilled());
    }

    IEnumerator Refilled()
    {
        rifle = GameObject.Find("Muzzle").GetComponent<Rifle>();

        // For Pistol Refill
        if (rifle.RifletotalAmmo == rifle.RifleFulltotalAmmo)
        {
            AmmoFull.gameObject.SetActive(true);

            yield return new WaitForSeconds(timeAmmoFull);

            AmmoFull.gameObject.SetActive(false);
        }
        else if (rifle.RifletotalAmmo < rifle.RifleFulltotalAmmo)
        {
            CountDown.gameObject.SetActive(true);

            RefillRifleTotalAmmo = 30;
            rifle.RifletotalAmmo = rifle.RifletotalAmmo + RefillRifleTotalAmmo;
            rifle.RifleTotalAmmo.text = rifle.RifletotalAmmo.ToString();

            yield return new WaitForSeconds(timeRefilled);

            CountDown.gameObject.SetActive(false);
        }
    }
}