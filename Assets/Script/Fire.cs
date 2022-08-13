using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fire : MonoBehaviour
{
    public string currentState;
    public string nextState;

    public TextMeshProUGUI HealthDigit;
    float PlayerHealth = 100;

    public float HealthTime;
    public float HurtTime;

    [SerializeField]
    private bool pain;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            pain = true;
            
            if (pain)
            {
                SwitchState();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            if (pain)
            {
                pain = false;
                SwitchState();
            }
        }
    }

    private void Start()
    {
        currentState = "notPain";
        nextState = currentState;
    }

    private void Update()
    {
        if (pain)
        {
            nextState = "Pain";
        }
        else
        {
            nextState = "notPain";
        }

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }

    void SwitchState()
    {
        StartCoroutine(currentState);
    }

    IEnumerator Pain()
    {
        while (currentState == "Pain")
        {
            yield return new WaitForSeconds(HurtTime);

            if (PlayerHealth <= 100)
            {
                --PlayerHealth;
                HealthDigit.text = PlayerHealth.ToString();
            }
        }
        SwitchState();
    }

    IEnumerator notPain()
    {
        while (currentState == "notPain")
        {
            yield return new WaitForSeconds(HealthTime);

            if (PlayerHealth < 100)
            {
                ++PlayerHealth;
                HealthDigit.text = PlayerHealth.ToString();
            }
            else if (PlayerHealth == 100)
            {
                PlayerHealth = 100;
                HealthDigit.text = PlayerHealth.ToString();
            }
        }
        SwitchState();
    }
}
