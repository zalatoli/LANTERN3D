using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    //Invincibility upon taking damage
    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    //count down until next flash state transition
    private float flashCounter;
    //Controls the length of transition between flashes
    //when player takes damage
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;

    public float respawnLength;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            //When the flash counter reaches zero the player
            //will switch between visible/invisible until
            //the invincibility ends
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }
            //Make sure the player is visible when 
            //invincibility ends
            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
		
	}
    public void HurtPlayer(int damage, Vector3 direction)
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                thePlayer.Knockback(direction);
                invincibilityCounter = invincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }

        }
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void Respawn()
    {

        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
        
    }
    //IEnumerator is a coroutine which operates on its
    //own timeline
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;
    }
}
