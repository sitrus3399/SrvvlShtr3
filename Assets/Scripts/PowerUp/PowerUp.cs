using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    public int powerNumber;
    Light powerLight;
    bool active = true;
    private IEnumerator countdown;
    void Awake()
    {
        //Mendapatkan komponen player health
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        powerLight = GetComponent<Light>();
        countdown = timer(3f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player && active)
        {
            switch (powerNumber)
            {
                case 1:
                    playerMovement.speed *= 2;
                    powerLight.enabled = false;
                    active = false;
                    StartCoroutine(countdown);
                    break;
                case 2:
                    playerHealth.currentHealth += 50;
                    powerLight.enabled = false;
                    active = false;
                    if (playerHealth.currentHealth > playerHealth.startingHealth)
                    {
                        playerHealth.currentHealth = playerHealth.startingHealth;
                    }
                    playerHealth.healthSlider.value = playerHealth.currentHealth;
                    Destroy(gameObject);
                    break;
            }
        }
    }

    IEnumerator timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerMovement.speed /= 2;
        Destroy(gameObject);
    }
}
