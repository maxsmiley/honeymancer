using UnityEngine;
using System.Collections;

public class Honeycomb : MonoBehaviour
{
	public Transform honeycombpop;
	public bool popped_out = false; //Whether the honeycomb has been popped out or not
	bool moused_over = false;//
	private Mancy mancy; //Player's mancy 

	//For calculating if mouse over object
	Ray ray;
	RaycastHit hit;

	private Pauser pauser;

	void Awake()
	{
		pauser = GameObject.Find("Pauser").GetComponent<Pauser>();
		mancy = GameObject.Find("Mancy").GetComponent<Mancy>();
		//collider2D.enabled = false;
	}

	void OnMouseOver()
	{
		moused_over = true;
		PopOut ();
	}

	void OnMouseExit(){
		moused_over = false;
	}
	

	void PopOut (){
		if ( !popped_out && Input.GetMouseButton(0) && mancy.mancy > mancy.mancy_cost && !pauser.paused) {
			Transform popped;
			mancy.mancy -= mancy.mancy_cost;
			popped_out = true;
		 	popped = Instantiate(honeycombpop, transform.position, transform.rotation) as Transform;

			Destroy (popped.gameObject, 2);
		}
	}

}