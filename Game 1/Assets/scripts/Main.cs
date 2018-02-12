using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
   public GameObject player1;
	public	GameObject enemy;
	public	GameObject wall;
	public	GameObject key;
	public	GameObject door; 
	public	GameObject exit;
	public  GameObject checkpoint;



	int[][] gameBoard;

	int playerX;
	int playerY;
	int playerJumpSpeed;
	int playerJumpleft;
	GameObject player;

	int lastCeckPointX;
	int lastCeckPointY;


	public GameObject youlose;
	public GameObject youWin;
	GameObject door1;
	GameObject key1;


	int gameState = 0;

	long lastCheck;

	/*0 frei
	 *1 wand
	 *2 leiter 
	 *3 Gegner 
	 *4 Ziel
	 *5 StartSpieler
	 *100-199 Schluessel
	 *200-299 Tueren
	 *
	 *
	 *
	 * */


	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3 (48 * 0.5f, 27 * 0.5f, -25f);
		gameBoard = new int[48] [];
		for (int i = 0; i < 48; i++) {
			gameBoard [i] = new int[27];
		}

		for(int x =0; x < 48; x++) {
			for (int y = 0;y <27; y++) {
				gameBoard [x] [y] = 0;

			}
				}

		for(int x =0; x < 48; x++) {
			gameBoard [x] [0] = 1;
			gameBoard [x] [26] = 1;
		}

		for (int y = 0; y < 27; y++) {
			gameBoard [0] [y] = 1;
			gameBoard [47] [y] = 1;
		}

		gameBoard [2] [2] = 5;


		gameBoard [1] [1] = 1;
		gameBoard [2] [1] = 1;
		gameBoard [5] [2] = 1;
		gameBoard [6] [2] = 1;
		gameBoard [9] [3] = 1;
		gameBoard [10] [3] = 1;
		gameBoard [12] [4] = 1;
		gameBoard [13] [4] = 1;
		gameBoard [15] [5] = 1;
		gameBoard [16] [5] = 1;
		gameBoard [17] [5] = 1;
		gameBoard [18] [8] = 1;
		gameBoard [19] [8] = 1;
		gameBoard [20] [8] = 1;
		gameBoard [20] [7] = 1;
		gameBoard [20] [6] = 1;
		gameBoard [20] [5] = 1;
		gameBoard [20] [4] = 1;
		gameBoard [20] [3] = 1;
		gameBoard [20] [2] = 1;
		gameBoard [20] [1] = 1;
		gameBoard [16] [11] = 1;
		gameBoard [15] [11] = 1;
		gameBoard [14] [11] = 1;
		gameBoard [13] [11] = 1;
		gameBoard [12] [11] = 1;
		gameBoard [10] [11] = 1;
		gameBoard [8] [11] = 1;
		gameBoard [7] [11] = 1;
		gameBoard [6] [11] = 1;
		gameBoard [5] [11] = 1;
		gameBoard [4] [11] = 1;
		gameBoard [3] [11] = 1;
		gameBoard [2] [11] = 1;
		gameBoard [1] [11] = 1;
		gameBoard [5] [13] = 1;
		gameBoard [6] [13] = 1;
		gameBoard [6] [14] = 1;
		gameBoard [6] [15] = 1;
		gameBoard [6] [16] = 1;
		gameBoard [6] [17] = 1;
		gameBoard [6] [18] = 1;
		gameBoard [6] [19] = 1;
		gameBoard [6] [20] = 1;
		gameBoard [6] [21] = 1;
		gameBoard [6] [22] = 1;
		gameBoard [6] [23] = 1;
		gameBoard [6] [24] = 1;
		gameBoard [1] [16] = 1;
		gameBoard [5] [18] = 1;
		gameBoard [1] [21] = 1;
		gameBoard [5] [24] = 1;
		gameBoard [23] [4] = 3;
		gameBoard [25] [1] = 6;
		gameBoard [27] [1] = 1;
		gameBoard [10] [15] = 1;
		gameBoard [7] [14] = 1;
		gameBoard [12] [18] = 8;
		gameBoard [24] [1] = 7;
































		gameBoard [26] [14] = 4;
		for (int x = 24; x < 44; x++) {
			gameBoard [x] [13] = 1;
		}
		for (int x = 46; x < 48; x++) {
			gameBoard [x] [11] = 1;
		}
		for (int y = 2; y < 9; y++) {
			gameBoard [43] [y] = 1;
		}
		for (int y = 2; y < 9; y++) {
			gameBoard [42] [y] = 1;
		}
		for (int y = 2; y < 7; y++) {
			gameBoard [39] [y] = 1;
		}
		for (int y = 2; y < 7; y++) {
			gameBoard [38] [y] = 1;
		}
		for (int y = 2; y < 7; y++) {
			gameBoard [34] [y] = 1;
		}
		for (int y = 2; y < 7; y++) {
			gameBoard [33] [y] = 1;
		}
		for (int y = 2; y < 5; y++) {
			gameBoard [31] [y] = 1;
		}
		for (int y = 2; y < 5; y++) {
			gameBoard [30] [y] = 1;
		}
		for (int x = 28; x < 47; x++) {
			gameBoard [x] [1] = 3;
		}
		for (int x = 7; x < 23; x++) {
			gameBoard [x] [24] = 1;
		}
		for (int y = 2; y < 26; y++) {
			gameBoard [24] [y] = 1;
		}
		for (int y = 3; y < 25; y++) {
			gameBoard [20] [y] = 1;
		}
		for (int x = 3; x < 20; x++) {
			gameBoard [x] [1] = 3;
		}



		gameBoard [13] [12] = 6;

		gameBoard[11] [1] = 3;


		generateObjects ();

		youlose.transform.position = new Vector3 (-100,-100,-100);
		youWin.transform.position = new Vector3 (-100,-100,-100);
	}
	public void generateObjects()
	{
		for (int x = 0; x < 48; x++) {
			for (int y = 0; y < 27; y++) {
				if (gameBoard [x] [y] == 1) {
					Instantiate (wall, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard[x] [y] == 3) {
					Instantiate (enemy, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard[x] [y] == 4) {
					Instantiate (exit, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard[x] [y] == 6) {
					Instantiate (checkpoint, new Vector3 (x, y, 0), Quaternion.identity);
				}
			
			if (gameBoard[x] [y] == 7) {
					door1 = (GameObject) Instantiate (door, new Vector3 (x, y, 0), Quaternion.identity);
			}
			if (gameBoard[x] [y] == 8) {
					key1  = (GameObject) Instantiate (key, new Vector3 (x, y, 0), Quaternion.identity);
			}

				if (gameBoard [x] [y] == 5 && player == null) {
					player = (GameObject)Instantiate (player1, new Vector3 (x, y, 0), Quaternion.identity);
					playerX = x;
					playerY = y;
					lastCeckPointX = x;
					lastCeckPointY = y;
				}
			}
		}
	}



	// Update is called once per frame
	void Update () {
		long now = System.DateTime.Now.Ticks;

		if (now > lastCheck + 500000) {
			lastCheck = now;

			if (gameState == -1 || gameState == 1) {
				if (Input.GetKey ("n")) {
					playerX = lastCeckPointX;
					playerY = lastCeckPointY;
					gameState = 0;
					youlose.transform.position = new Vector3 (-100,-100,-100);
					youWin.transform.position = new Vector3 (-100,-100,-100);
				}
			}

			if(gameState ==0)
			{
			if (gameBoard [playerX] [playerY - 1] == 1) {
				//stop,wenn auf Boden
				if (playerJumpSpeed < 0) {
					playerJumpSpeed = 0;
				}
				if (playerJumpSpeed == 0) {
					playerJumpleft = 1;
				}
			}	
			//Wand test
			playerJumpSpeed--;
			if (playerJumpSpeed > 0) {
				for (int y = 0; y < playerJumpSpeed + 1; y++) {
					if (gameBoard [playerX] [playerY + y] == 1) {
						playerJumpSpeed = y - 1;
					}
				}
			}
			if (playerJumpSpeed < 0) {
				for (int y = 0; y > playerJumpSpeed - 1; y--) {
					if (gameBoard [playerX] [playerY + y] == 1) {
						playerJumpSpeed = y + 1;
					}
				}
			}


			playerY += playerJumpSpeed;
			//Sprungtaste
			if (Input.GetKey ("space") && playerJumpleft > 0) {
				playerJumpSpeed = 3;
				playerJumpleft--;
			}
			//rechts bewegen
			if (Input.GetKey ("d")) {
				if (gameBoard [playerX + 1] [playerY] == 1 || gameBoard [playerX + 1] [playerY] == 7) {
					
				} else {
					playerX++;

							
				}
				//vertikal bewegung (hoch runter)
			}//links bewegen
			if (Input.GetKey ("a")) {
				if (gameBoard [playerX - 1] [playerY] == 1 || gameBoard [playerX - 1] [playerY] == 7) {

				} else {
					playerX--;


				}
			}
			if (gameBoard [playerX] [playerY] == 4) {
				gameState = 1;
				youWin.transform.position = new Vector3 (24, 13, 0);
				youWin.transform.eulerAngles = new Vector3 (-90, 0, 0);
				youWin.transform.localScale = new Vector3 (4, 2, 2);
			}
			if (gameBoard [playerX] [playerY] == 3) {
				gameState = -1;
				youlose.transform.position = new Vector3 (24, 13, 0);
				youlose.transform.eulerAngles = new Vector3 (-90, 0, 0);
				youlose.transform.localScale = new Vector3 (4, 2, 2);
			}
				if (gameBoard [playerX] [playerY] == 8) {
					Destroy (key1);
					Destroy (door1);
					for (int x = 0; x < 48; x++) {
						for (int y = 0; y < 27; y++) {
							if(gameBoard [x] [y] == 7) {
								gameBoard[x][y] = 0;
							}
						}
					}
				}
					

				if (gameBoard [playerX] [playerY] == 6) {
					lastCeckPointX = playerX;
					lastCeckPointY = playerY;
				
				
				}
			player.transform.position = new Vector3 (playerX, playerY, 0);
		}
		}
	}
}