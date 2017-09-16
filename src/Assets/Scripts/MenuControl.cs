using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour
{	
	void OnMouseUpAsButton() {
		Application.LoadLevel("Level");
	}
}
