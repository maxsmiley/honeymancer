using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float health_max = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 15f;			// The amount of damage to take when enemies touch the player

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player


	private float barsize = 0f;
	public SpriteRenderer mancyBar;
	private float healthBarLength = 0f;

	public Texture2D background; // Allows you to place a textuer in, in the Inspector
	public Texture2D foreground;//Allows you to place a texture in, in th;e inspector
	public Texture2D lion;//Allows you to place a texture in, in th;e inspector

	private float width = 10f;
	Mancy mancy;

	void Awake ()
	{
		mancy = GameObject.Find("Mancy").GetComponent<Mancy>();
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		mancyBar = GameObject.Find("MancyBar").GetComponent<SpriteRenderer>();
		healthScale = mancyBar.transform.localScale;
		barsize = Screen.height * .8f;
		healthBarLength = barsize;
	
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		//healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		//anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		//healthScale = healthBar.transform.localScale;

		UpdateHealthBar ();
	}
	void Update(){
		if (mancy.mancy == mancy.mancy_max & health < health_max) {
			health += 0.25f;
			UpdateHealthBar ();
		}
	}
	void OnGUI()// set up for working with items in the GUI
	{
		
		//GUI.DrawTexture(new Rect(30, 10,Screen.width/5, 20), background);
		GUI.color = new Vector4(0.5F, 0.5F, 0.5F, 1);
		GUI.DrawTexture(new Rect(Screen.width - 30, Screen.height - barsize - 30, width, barsize), foreground,ScaleMode.StretchToFill);
		
		//GUI.DrawTexture(new Rect(10, 10,Screen.width/5, 20), lion);
		GUI.color = new Vector4(1F, 0.5F, 0.5F, 1);
		if (health > 0f){
			GUI.DrawTexture(new Rect(Screen.width - 30, Screen.height - 30 - healthBarLength, width, healthBarLength), foreground,ScaleMode.StretchToFill);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					// Find all of the colliders on the gameobject and set them all to be triggers.
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Move all sprite parts of the player to the front
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in spr)
					{
						s.sortingLayerName = "UI";
					}

					// ... disable user Player Control script
					GetComponent<PlayerControl>().enabled = false;

					// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
				//	GetComponentInChildren<Gun>().enabled = false;

					// ... Trigger the 'Die' animation state
					anim.SetTrigger("Die");
				}
			}
		}
	}


	void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		rigidbody2D.AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		//AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}





	public void UpdateHealthBar ()
	{

		healthBarLength = (barsize) * (health / (float)health_max); // The full length of the bar * % of cur health.

		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		//healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		//healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
