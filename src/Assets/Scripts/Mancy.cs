using UnityEngine;
using System.Collections;

public class Mancy : MonoBehaviour
{
	public float mancy = 6f;					// The player's score.
	public float mancy_max = 12f;
	public float mancy_rate = 0.01f;
	private float mancy_rate_cur = 0.0f;
	public float mancy_per_rate = 1f;
	public float mancy_cost = 1f;
	public float mancyBarLength;//The length of the health bar.
	public Texture2D background; // Allows you to place a textuer in, in the Inspector
	public Texture2D foreground;//Allows you to place a texture in, in th;e inspector
	public Texture2D lion;//Allows you to place a texture in, in th;e inspector

	public float barsize = 0f;
	
	private PlayerControl playerControl;	// Reference to the player control script.
	private float previousScore = 0f;			// The score in the previous frame.

	//Bar
	private SpriteRenderer mancyBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 mancyScale;				// The local scale of the health bar initially (with full health).

	private float width = 10f;
	
	void Awake ()
	{
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		mancyBar = GameObject.Find("MancyBar").GetComponent<SpriteRenderer>();
		mancyScale = mancyBar.transform.localScale;
		barsize = Screen.height * .8f;
		mancyBarLength = barsize;
	}
	

	void OnGUI()// set up for working with items in the GUI
	{

		//GUI.DrawTexture(new Rect(30, 10,Screen.width/5, 20), background);
		GUI.color = new Vector4(0.5F, 0.5F, 0.5F, 1);
		GUI.DrawTexture(new Rect(Screen.width - width - 30, Screen.height - barsize - 30, width, barsize), foreground,ScaleMode.StretchToFill);

		//GUI.DrawTexture(new Rect(10, 10,Screen.width/5, 20), lion);
		GUI.color = new Vector4(0.5F, 1, 0.5F, 1);
		if (mancy > 0){
			GUI.DrawTexture(new Rect(Screen.width - width - 30, Screen.height - 30 - mancyBarLength, width, mancyBarLength), foreground,ScaleMode.StretchToFill);
		}
	}


	void Update ()
	{
		
		mancy_rate_cur += Time.deltaTime;
		if(mancy_rate_cur > mancy_rate){
			mancy_rate_cur-= mancy_rate;
			if(mancy < mancy_max){
				//mancy += mancy_per_rate;
				AddjustCurrentHealth(mancy_per_rate);

			}
			//UpdateMancyBar();

		}
		// Set the score text.
		guiText.text = "Macy: " + mancy;
		
		// If the score has changed...
		//if(previousScore != score)
		// ... play a taunt.
		//playerControl.StartCoroutine(playerControl.Taunt());
		
		// Set the previous score to this frame's score.
		previousScore = mancy;

	}

	public void AddjustCurrentHealth(float adj)//This function will allows us to alter our current health outside this script.
	{
		
		mancy += adj;//This is to recieve heals or dammage to the CurHealth.  The number is passed in then assigned to curHealth.
		
		if(mancy < 0)//Checks if the players health has gone below 0.
			mancy = 0;// If players health has gone below 0 set it to 0.
		
		if(mancy> mancy_max)//Checks if player health is higher then maxHealth.
			mancy = mancy_max;//If players health is higher then maxHealth set it = to maxHeatlh
		
		if(mancy_max <1)//Checks if maxHealth is set to less then 1.
			mancy_max = 1;//If maxHealth is set below 1, this sets it to 1.
		
		mancyBarLength = (barsize) * (mancy / (float)mancy_max); // The full length of the bar * % of cur health.
		
	}
	
	public void UpdateMancyBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		mancyBar.material.color = Color.Lerp(Color.green, Color.red, mancy_max - mancy * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		mancyBar.transform.localScale = new Vector3(mancyScale.x * 15 * mancy * 0.01f, 0.5f, 1);
	}
}
	
