using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	public bool paused = false;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			paused = !paused;
		}

		/*if (paused && Input.GetMouseButton (0)) {
			paused = false;
		}*/

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	void OnGUI () {
		if (!paused) {
			if (GUI.Button (new Rect (20, 40, 80, 20), "Pause")) {
					paused = !paused;
			}
		} else {
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 20, 80, 20), "Continue")) {
					paused = !paused;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 20 +30, 80, 20), "Menu")) {
					Application.LoadLevel("Menu");
				}
			}
		}
	}
