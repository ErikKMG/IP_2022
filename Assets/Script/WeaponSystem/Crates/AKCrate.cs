using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AKCrate : MonoBehaviour
{
    // Link to the Pistol Raycast object
    private AK ak;

    int RefillAKTotalAmmo;

    // Time to wait to get Refilled again
    public float timeRefilled;

    // Time for AmmoFull
    public float timeAmmoFull;

    // Count Down UI
    public TextMeshPro CountDown;

    // Full Ammo UI
    public TextMeshPro AmmoFull;

    public void Refill(AK newAK)
    {
        ak = newAK;

        StartCoroutine(Refilled());
    }

    IEnumerator Refilled()
    {
        ak = GameObject.Find("Muzzle").GetComponent<AK>();

        // For Pistol Refill
        if (ak.AKtotalAmmo == ak.AKFulltotalAmmo)
        {
            AmmoFull.gameObject.SetActive(true);

            yield return new WaitForSeconds(timeAmmoFull);

            AmmoFull.gameObject.SetActive(false);
        }
        else if (ak.AKtotalAmmo < ak.AKFulltotalAmmo)
        {
            CountDown.gameObject.SetActive(true);

            RefillAKTotalAmmo = 30;
            ak.AKtotalAmmo = ak.AKtotalAmmo + RefillAKTotalAmmo;
            ak.AKTotalAmmo.text = ak.AKtotalAmmo.ToString();

            yield return new WaitForSeconds(timeRefilled);

            CountDown.gameObject.SetActive(false);
        }
    }
}