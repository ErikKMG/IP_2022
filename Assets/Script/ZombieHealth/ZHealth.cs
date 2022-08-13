using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZHealth : MonoBehaviour
{
    // Bool to check of Player collided on it
    public bool isChange;

    // Health Cube itself
    public GameObject self;

    // Color Array
    public Color[] ChangeColor;

    private int ColorIndex = 0;

    // Idle Color
    [SerializeField] private Color idleColor;

    // Health UI
    public TextMeshPro HealthUI;
    float Health = 4;

    // Mesh Renderer
    public MeshRenderer myRenderer;

    // Time To Wait In Seconds
    public float TimeSeconds;


    // Start is called before the first frame update
    void Start()
    {
        myRenderer.material.color = idleColor;
    }

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
                Destroy(self);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
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
                Destroy(self);
            }
        }
    }



    // Change Green to Yellow
    IEnumerator Change()
    {
        isChange = true;

        if (isChange)
        {

            myRenderer.material.color = ChangeColor[ColorIndex];

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

            myRenderer.material.color = ChangeColor[ColorIndex];

            --Health;
            HealthUI.text = Health.ToString();

            yield return new WaitForSeconds(TimeSeconds);

            isChange = false;


            --ColorIndex;
            myRenderer.material.color = ChangeColor[ColorIndex];

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
            myRenderer.material.color = ChangeColor[ColorIndex];
            --Health;
            HealthUI.text = Health.ToString();

            yield return new WaitForSeconds(TimeSeconds);

            isChange = false;

            --ColorIndex;
            myRenderer.material.color = ChangeColor[ColorIndex];
            ++Health;
            HealthUI.text = Health.ToString();
            yield return new WaitForEndOfFrame();
        }
    }
}