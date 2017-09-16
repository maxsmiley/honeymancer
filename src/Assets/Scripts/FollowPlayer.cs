using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	public bool y_only = false;		// Sets whether x-offset should matter.

	private Transform player;		// Reference to the player.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
	{
		// Set the position to the player's position with the offset.
		if (!y_only) {
			transform.position = player.position + offset;

		}
		// Set the position to have a static x pos.
		else {
			if (player != null){
				float x, y, z;
				x = transform.position.x;
				y = player.position.y + offset.y;
				z = transform.position.z;
				Vector3 newpos = new Vector3(x, y, z);
				
				transform.position = newpos;	
			}
		}
	}
}
