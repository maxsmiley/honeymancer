using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float timer = 0; // how long you want the item to last
	private float startTime;

	void FixedUpdate()
	{
		if (Time.time - startTime > 1) {
			Debug.Log ("disentigrate");
			Destroy (this);
		}
	}
	
}