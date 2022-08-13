using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZHealth : MonoBehaviour
{
    // Bool to check of Player collided on it
    public bool isChange;

    // Color Array
    public Color[] ChangeColor;

    private int ColorIndex = 0;

    // Sprit Color
    public SpriteRenderer HealthBar;

    // Health UI
    public TextMeshPro HealthUI;
    float Health = 4;

    // Time To Wait In Seconds
    public float TimeSeconds;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (Health == 4)
            {
                // Green to Yellow
                StartCoroutine(Change());
            }
            else if (Health == 3)
            {
                // Yellow to Orange
                StartCoroutine(ChangeOne());
            }
            else if (Health == 2)
            {
                // Orange to Red
                StartCoroutine(ChangeTwo());
            }
            else if (Health == 1)
            {
                // Destry Helth Cube
                Destroy(gameObject);
            }
        }
    }

    // Change Green to Yellow
    IEnumerator Change()
    {
        isChange = true;

        if (isChange)
        {

            HealthBar.material.color = ChangeColor[ColorIndex];

            --Health;
            HealthUI.text = Health.ToString();

            yield return new WaitForSeconds(TimeSeconds);

            isChange = false;
        }
    }

    // Change Yellow to Orange
    IEnumerator ChangeOne()
    {
        isChange = true;

        if (isChange)
        {
            ++ColorIndex;

            HealthBar.material.color = ChangeColor[ColorIndex];

            --Health;
            HealthUI.text = Health.ToString();

            yield return new WaitForSeconds(TimeSeconds);

            isChange = false;


            --ColorIndex;
            HealthBar.material.color = ChangeColor[ColorIndex];

            ++Health;
            HealthUI.text = Health.ToString();
            yield return new WaitForEndOfFrame();
        }
    }

    // Change Orange to Red
    IEnumerator ChangeTwo()
    {
        isChange = true;

        if (isChange)
        {
            ++ColorIndex;
            HealthBar.material.color = ChangeColor[ColorIndex];
            --Health;
            HealthUI.text = Health.ToString();

            yield return new WaitForSeconds(TimeSeconds);

            isChange = false;

            --ColorIndex;
            HealthBar.material.color = ChangeColor[ColorIndex];
            ++Health;
            HealthUI.text = Health.ToString();
            yield return new WaitForEndOfFrame();
            //yield return ToOrange();
        }
    }
}