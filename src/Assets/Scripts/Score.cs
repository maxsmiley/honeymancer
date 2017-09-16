using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public float score = 0f;					// The player's score.

	private float time_cur = 0;
	private PlayerControl playerControl;	// Reference to the player control script.
	private float previousScore = 0f;			// The score in the previous frame.
	private Mancy mancy;

	void Awake ()
	{
		mancy = GameObject.Find("Mancy").GetComponent<Mancy>();
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update ()
	{

		time_cur += Time.deltaTime;
		if(time_cur > 0.1f){
			time_cur-= 0.1f;
			score += (mancy.mancy/10f);
		}
		// Set the score text.

		guiText.text = "Score: " + (int)score/10;

		// If the score has changed...
		if(previousScore != score)
			// ... play a taunt.
		/*	if (playerControl != null)
				playerControl.StartCoroutine(playerControl.Taunt());*/

		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
