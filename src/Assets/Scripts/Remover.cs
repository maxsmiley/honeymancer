using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
	public float TriggerSpeed;
	public float Accel;
	private float accel_cur = 0f;
	public float accel_rate = 1;

	void Awake()
	{
		rigidbody2D.AddForce(new Vector2(0f, TriggerSpeed));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// .. stop the Health Bar following the player
		/*	if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}
*/
			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			if(col.gameObject.tag == "Enemy"){

				// ... instantiate the splash where the enemy falls in.
				Instantiate(splash, col.transform.position, transform.rotation);

				// Destroy the enemy.
				Destroy (col.gameObject);	
			}
		}
	}

	void FixedUpdate ()
	{
		accel_cur += Time.deltaTime;
		if (accel_cur > accel_rate) {
			accel_cur -= accel_rate;
			rigidbody2D.AddForce (new Vector2 (0f, Accel));
		}

	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
