using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sigtrap.VrTunnellingPro;

public class PlayerHealth : MonoBehaviour {
    //This script handles the players health, displays it to the UI, adjusts the tunneling effect,
    //Heals, takes damage from smoke, coughs

    private TunnellingOpaque tunnellingScript;

    [Header("Health")]
    public static float healthPct;
    public static float health;
    public float maxHealth = 1000;
    public Text text;
    private HealthIndicator healthIndicatorScript;
    public float smokeDamage = 1;

    [Header("Sound")]
    [Tooltip("Step sound cooldown")]
    public float coughCD;
    //private bool firstCough = true;
    public AudioSource headSound;
    public AudioClip[] coughClip;
    private float coughTimer;

    private Coroutine h;

    void Start () {
		health = maxHealth;

		tunnellingScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TunnellingOpaque>();

        healthIndicatorScript = GetComponentInChildren<HealthIndicator>();

        h = StartCoroutine(Heal(1));    //heal every second

        coughTimer = coughCD;
    }

	//-----------------------------------------ALWAYS------------------------------------------//
	
	void Update () {
        //Debug.Log("Player Health: " + health);

        //ALWAYS DO:
        //Heal(); (in coroutine)           
        Cough();
		DisplayHealth();
    }

    IEnumerator Heal(int repeat)
    {
        if (health < maxHealth)
        {
            health++;       //heal 1 health each <repeat:
            StopCoroutine(h);
        }
        yield return new WaitForSeconds(repeat);
        StartCoroutine(Heal(1));
    }

	public void Cough() {

        /*if (firstCough)
        {
            firstCough = false;
            coughTimer = coughCD;
        }
        else
        {
        */

        //Debug.Log("qawerfe: " + coughTimer);
            //Cough SOUND (timer depends on health
            if (coughTimer <= 0)
            {
                Debug.Log("Coughing sound");

                headSound.clip = coughClip[Random.Range(0, coughClip.Length)];
                headSound.loop = true;
                headSound.PlayOneShot(headSound.clip);
                coughTimer = coughCD;
            }
            else
            {
            //Debug.Log("decrease timer");
                coughTimer -= Time.deltaTime;
            }
        //}
	}

	void DisplayHealth() {
		if (health > 0 && health <= maxHealth) {
			text.text = "" + health.ToString();
		}
	}

	//----------------------------------ONLY DO WHEN DAMAGED------------------------------------------//

	public void Damage() {
		if (health > 0) {
            //health--;
            //Debug.Log(health);
            health -= smokeDamage;

            //ONLY DO WHEN DAMAGED:
            UpdateHealth();
			AdjustTunneling();
        }
	}

	public void UpdateHealth() {
		healthPct = (float)health / (float)maxHealth; 
        healthIndicatorScript.HealthIndicatorUpdate(healthPct);

        //Adjust cough CD
        //coughCD = (1 + healthPct * 600) + 60;    

        //coughTimer = coughTimer * (healthPct);
        //Debug.Log("healthPCTY: " + healthPct);
        coughCD = coughCD * (healthPct);
    }

	void AdjustTunneling() {
		//Change effect feather & strengths of tunnel
		tunnellingScript.effectFeather = 0.1f + (((float)health / (float)maxHealth) / 10);
        tunnellingScript.angularVelocityStrength = 2 - ((float)health / (float)maxHealth);
        tunnellingScript.velocityStrength = 2 - ((float)health / (float)maxHealth);
    }
}
