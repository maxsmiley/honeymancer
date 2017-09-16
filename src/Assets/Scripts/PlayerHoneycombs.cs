using UnityEngine;
using System.Collections;

public class PlayerHoneycombs : MonoBehaviour {
	
	public Transform honeycombPrefab;
	public Transform poppedHoneycombPrefab;
	//public static GameState gameState;
	
	private Transform playerTrans;
	private float platformsSpawnedUpTo = 0.0f;
	private ArrayList honeycombs;
	private float nextPlatformCheck;
	private float honeycomb_height = 1.52f;
	
	
	void Awake () {
		playerTrans = transform;
		honeycombs = new ArrayList();
		platformsSpawnedUpTo = playerTrans.position.y- honeycomb_height*7;
		nextPlatformCheck = playerTrans.position.y + honeycomb_height * 2;
		SpawnHoneycombs(playerTrans.position.y + honeycomb_height * 8);
		//StartGame();
	}
/*	
	void StartGame()
	{
		Time.timeScale = 1.0f;
		//gameState = GameState.playing;
	}
	
	void GameOver()
	{
		Time.timeScale = 0.0f; //Pause the game
		gameState = GameState.gameover;
		GameGUI.SP.CheckHighscore();
	}
	*/
	void Update () {
		//Do we need to spawn new platforms yet? (we do this every X meters we climb)
		float playerHeight = playerTrans.position.y;
		if (playerHeight > nextPlatformCheck)
		{
			PlatformMaintenaince(); //Spawn new platforms
		}

		/*//Update camera position if the player has climbed and if the player is too low: Set gameover.
		float currentCameraHeight = transform.position.y;
		float newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
		if (playerTrans.position.y > currentCameraHeight)
		{
			transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
		}else{
			//Player is lower..maybe below the cameras view?
			if (playerHeight < (currentCameraHeight - 10))
			{
				//GameOver();
			}
		}
		
		//Have we reached a new score yet?
		if (playerHeight > GameGUI.score)
		{
			GameGUI.score = (int)playerHeight;
		}
			*/
	}
	
	
	
	void PlatformMaintenaince()
	{
		nextPlatformCheck += honeycomb_height*8;
		
		//Delete all platforms below us (save performance)
		for(int i = honeycombs.Count-1;i>=0;i--)
		{
			Transform plat = (Transform)honeycombs[i];
			if (plat.position.y < playerTrans.position.y - honeycomb_height*16)
			{
				//Debug.Log("Deleting");
				Destroy(plat.gameObject);
				honeycombs.RemoveAt(i);
			}            
		}
		
		//Spawn new platforms, 25 units in advance
		SpawnHoneycombs(nextPlatformCheck + honeycomb_height*8);
	}
	
	
	void SpawnHoneycombs(float upTo)
	{
		int width = 16;
		float honeycomb_width = 1.32f;
		float left = -9.5f;
		float left2 = left + honeycomb_width;

		float spawnHeight = platformsSpawnedUpTo;
		Vector3 pos;
		Transform plat;

		while (spawnHeight <= upTo)
		{
		
			for(float x = left - 6* honeycomb_width; x < left; x += honeycomb_width*2){
				pos = new Vector3(x, spawnHeight,0);
				if (Random.value < 0.95f){
					plat = (Transform)Instantiate(poppedHoneycombPrefab, pos, transform.rotation);
				}else{
					plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
				}
				honeycombs.Add(plat);
			}
			for(float x = left; x < left + (width * honeycomb_width); x += honeycomb_width*2){
				if (Random.value < 0.95f){
					pos = new Vector3(x, spawnHeight,0);
					plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
					honeycombs.Add(plat);
				}
			}
			for(float x = left + (width * honeycomb_width); x < left + (width * honeycomb_width) + 6* honeycomb_width; x += honeycomb_width*2){
				pos = new Vector3(x, spawnHeight,0);
				if (Random.value < 0.95f){
					plat = (Transform)Instantiate(poppedHoneycombPrefab, pos, transform.rotation);
				}else{
					plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
				}
				honeycombs.Add(plat);
			}
			spawnHeight += honeycomb_height/2;

			for(float x = left2 - 6* honeycomb_width; x < left2; x += honeycomb_width*2){
					pos = new Vector3(x, spawnHeight,0);
					if (Random.value < 0.95f){
						plat = (Transform)Instantiate(poppedHoneycombPrefab, pos, transform.rotation);
					}else{
						plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
					}
					honeycombs.Add(plat);
			}
			for(float x = left2; x < left2+( width* honeycomb_width); x += honeycomb_width*2){
				if (Random.value < 0.95f){
					pos = new Vector3(x, spawnHeight,0);
					plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
					honeycombs.Add(plat);
				}
			}
			for(float x = left2 + (width * honeycomb_width); x < left2 + (width * honeycomb_width) + 6* honeycomb_width; x += honeycomb_width*2){
				pos = new Vector3(x, spawnHeight,0);
				if (Random.value < 0.95f){
					plat = (Transform)Instantiate(poppedHoneycombPrefab, pos, transform.rotation);
				}else{
					plat = (Transform)Instantiate(honeycombPrefab, pos, transform.rotation);
				}
				honeycombs.Add(plat);
			}
			spawnHeight += honeycomb_height/2;

		}
		platformsSpawnedUpTo = upTo;
	}
	
}