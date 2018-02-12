using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public GameObject playerPrefab;
	public GameObject keyPrefab;
	public GameObject wallPrefab;
	public GameObject enemyPrefab;
	public GameObject doorPrefab;
	public GameObject exitPrefab;
	public GameObject deathPrefab;
	public GameObject cpPrefab;

	int[][] gameBoard;
	int levelBreite = 96;
	int levelHoehe = 54;

	int playerX;
	int playerY;
	int playerJumpSpeed;
	int playerJumpsLeft;
	GameObject player;

	int lastCheckPointX;
	int lastCheckPointY;

	public GameObject youLose;
	public GameObject youWin;

	GameObject door1;
	GameObject key1;


	int gameState = 0;
	//-1 tot
	//0 spiel läuft
	//1 gewonnen

	long lastCheck;

	//screen Location
	//x24 y13 z0 
	//rot x-90 y0 z0
	//scale x4 y2 z2

	/* 0	frei
	 * 1	wand
	 * 2	Leiter
	 * 3	Gegner
	 * 4	Ziel
	 * 5	StartSpieler
	 * 6	Death
	 * 7	Checkpoint
	 * 100 - 199	Schluessel
	 * 200 - 299	Türen
	 * */

	// Use this for initialization
	void Start () {

		//initialisiert SpielBrett
		gameBoard = new int[levelBreite][];
		for (int i = 0; i < levelBreite; i++) {
			gameBoard [i] = new int[levelHoehe];
		}
			
		//alles Luft
		for (int x = 0; x < levelBreite; x++) {
			for (int y = 0; y < levelHoehe; y++) {
				gameBoard [x] [y] = 0;

			}
		} 
		//wände waagerecht
		for (int x = 0; x < levelBreite; x++) {
			gameBoard [x] [0] = 1;
			gameBoard [x] [levelHoehe - 1] = 1;
		} 
		//Wände senkrecht
		for (int y = 0; y < levelHoehe; y++) {
			gameBoard [0] [y] = 1;
			gameBoard [levelBreite - 1] [y] = 1;
		}


		gameBoard [2] [2] = 5;

		//1. Säule:
		gameBoard [10] [3] = 1;
		gameBoard [11] [3] = 1;
		gameBoard [12] [3] = 1;
		gameBoard [13] [3] = 1;
		gameBoard [10] [2] = 1;
		gameBoard [11] [2] = 1;
		gameBoard [12] [2] = 1;
		gameBoard [13] [2] = 1;
		gameBoard [10] [1] = 1;
		gameBoard [11] [1] = 1;
		gameBoard [12] [1] = 1;
		gameBoard [13] [1] = 1;

		//2. Säule
		gameBoard [16] [5] = 1;
		gameBoard [17] [5] = 1;
		gameBoard [18] [5] = 1;
		gameBoard [19] [5] = 1;
		gameBoard [16] [4] = 1;
		gameBoard [17] [4] = 1;
		gameBoard [18] [4] = 1;
		gameBoard [19] [4] = 1;
		gameBoard [16] [3] = 1;
		gameBoard [17] [3] = 1;
		gameBoard [18] [3] = 1;
		gameBoard [19] [3] = 1;
		gameBoard [16] [2] = 1;
		gameBoard [17] [2] = 1;
		gameBoard [18] [2] = 1;
		gameBoard [19] [2] = 1;
		gameBoard [16] [1] = 1;
		gameBoard [17] [1] = 1;
		gameBoard [18] [1] = 1;
		gameBoard [19] [1] = 1;

		//3. Säule
		for (int x = 22; x < 26; x++) {
			gameBoard [x] [1] = 1;
			gameBoard [x] [2] = 1;
			gameBoard [x] [3] = 1;
			gameBoard [x] [4] = 1;
			gameBoard [x] [5] = 1;
			gameBoard [x] [6] = 1;
		}
		//4. Säule
		for (int x = 29; x < 34; x++) {
			gameBoard [x] [1] = 1;
			gameBoard [x] [2] = 1;
			gameBoard [x] [3] = 1;
			gameBoard [x] [4] = 1;
			gameBoard [x] [5] = 1;
			gameBoard [x] [6] = 1;
			gameBoard [x] [7] = 1;
		}

		gameBoard [34] [10] = 1;
		gameBoard [33] [7] = 1;
		gameBoard [34] [7] = 1;
		gameBoard [35] [7] = 1;
		gameBoard [36] [7] = 7;


		//Great Wall
		for (int y = 9; y < 53; y++) {
			gameBoard [34] [y] = 1;
			gameBoard [35] [y] = 1;
			gameBoard [36] [y] = 1;
		}
		//Sprünge zum key
		for (int x = 25; x < 28; x++) {
			gameBoard [x] [11] = 1;	
		}

		for (int x = 21; x < 24; x++) {
			gameBoard [x] [13] = 1;	
		}
		for (int x = 18; x < 21; x++) {
			gameBoard [x] [15] = 1;	
		}
			gameBoard [14] [16] = 1;	
		    gameBoard [15] [16] = 7;

		for (int x = 11; x < 13; x++) {
			gameBoard [x] [18] = 1;	
		}
		for (int x = 7; x < 9; x++) {
			gameBoard [x] [20] = 1;	
		}
		for (int x = 4; x < 6; x++) {
			gameBoard [x] [22] = 1;	
		}
		gameBoard [1] [24] = 1;


		//ziel:
		gameBoard [levelBreite - 2] [1] = 4;
		gameBoard [levelBreite - 3] [1] = 4;


		//death:
		gameBoard [42] [1] = 6;
		for (int x = 33; x < 45; x++) {
			gameBoard [x] [1] = 6;
		}
		gameBoard [14] [1] = 6;
		gameBoard [15] [1] = 6;
		gameBoard [20] [1] = 6;
		gameBoard [21] [1] = 6;
		gameBoard [26] [1] = 6;
		gameBoard [27] [1] = 6;
		gameBoard [28] [1] = 6;


		gameBoard [50] [1] = 6;

		gameBoard [53] [1] = 6;
		gameBoard [54] [1] = 6;

		gameBoard [58] [1] = 6;
		gameBoard [59] [1] = 6;

		gameBoard [63] [1] = 6;
		gameBoard [66] [1] = 6;




		//Schlüssel
		gameBoard [1] [25] = 100;


		//Türen
		gameBoard [34] [8] = 200; 



		generateObject ();
	

		youLose.transform.position = new Vector3 (-100, -100, -100);
		youWin.transform.position = new Vector3 (-100, -100, -100);

	}

	public void generateObject()
	{
		for (int x = 0; x < levelBreite; x++) {
			for (int y = 0; y < levelHoehe; y++) {
				if (gameBoard [x] [y] == 1) {
					Instantiate (wallPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard [x] [y] == 5 && player == null) {
					player = (GameObject) Instantiate (playerPrefab, new Vector3 (x, y, 0), Quaternion.identity);
					playerX = x;
					playerY = y;
					lastCheckPointX = x;
					lastCheckPointY = y;	
				}
				if (gameBoard [x] [y] == 4) {
					Instantiate (exitPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard [x] [y] == 6) {
					Instantiate (deathPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard [x] [y] == 7) {
					Instantiate (cpPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard [x] [y] == 100) {
					key1 = (GameObject) Instantiate (keyPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}
				if (gameBoard [x] [y] == 200) {
					door1 = (GameObject) Instantiate (doorPrefab, new Vector3 (x, y, 0), Quaternion.identity);
				}

			}

		}

	}


	// Update is called once per frame
	void Update () {
		long now = System.DateTime.Now.Ticks;

		if (now > lastCheck + 300000) {
			lastCheck = now;

			if (gameState == -1 || gameState == 1) {
				if (Input.GetKey ("p")) {
					playerX = lastCheckPointX;
					playerY = lastCheckPointY;
					gameState = 0;
					youLose.transform.position = new Vector3 (-100, -100, -100);
					youWin.transform.position = new Vector3 (-100, -100, -100);
				}

			}
			else if (gameState == 0)
				//landen (nach sprung)
			{
				if (gameBoard [playerX] [playerY - 1] == 1 || gameBoard [playerX] [playerY - 1] == 7) {
				playerJumpsLeft = 1;
				if (playerJumpSpeed < 0) {
					playerJumpSpeed = 0;
				}
				if (playerJumpSpeed == 0) {
					playerJumpsLeft = 1;
				}
			}
			//WandÜberprüfung
			// verkleinert Jumpspeed wenn kein Boden
			playerJumpSpeed--;
			if (playerJumpSpeed > 0) {
				for (int y = 0; y < playerJumpSpeed + 1; y++) {
						if (gameBoard [playerX] [playerY + y] == 1 || gameBoard [playerX] [playerY + y] == 7) {
						playerJumpSpeed = y - 1;

					}
				}
			}
				//fallen
				//wenn playerJumpSpeed kleiner als 0
			if (playerJumpSpeed < 0) {
					//Schleife, die y ersellt und auf 0 setzt, vergleicht y mit Playerjumpspeed -1, falls größer y - 1!
				for (int y = 0; y > playerJumpSpeed - 1; y--) {
						//fragt Koordinaten des Spielers ab, PlayerY + y , wenn da Wand, dann nächste Zeile
						if (gameBoard [playerX] [playerY + y] == 1 || gameBoard [playerX] [playerY + y] ==  7) {
							//setzt playerJumpSpeed auf y + 1 (y ist in diesem falll der boden, y + 1 ist der Luftblock überm boden)
						playerJumpSpeed = y + 1;

					}
				}
			}

			//vertikalbewegung (sprung)

			playerY += playerJumpSpeed;

			//Sprung
				if (Input.GetKey ("space") && playerJumpsLeft > 0) {
				playerJumpSpeed = 3;
				playerJumpsLeft--;
		
			}
			//nach rechts bewegen
			if (Input.GetKey ("d")) {
				//ist rechts Wand oder Tür?
				if (gameBoard [playerX + 1] [playerY] == 1 || gameBoard [playerX + 1] [playerY] >= 200) {

				} else {
					playerX++;
				}
		
			}
			//nach links bewegen
			if (Input.GetKey ("a")) {
				if (gameBoard [playerX - 1] [playerY] == 1 || gameBoard [playerX - 1] [playerY] >= 200) {

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

			if (gameBoard [playerX] [playerY] == 6) {
				gameState = -1;
				youLose.transform.position = new Vector3 (24, 13, 0);
				youLose.transform.eulerAngles = new Vector3 (-90, 0, 0);
				youLose.transform.localScale = new Vector3 (4, 2, 2);
			}
			
				if (gameBoard [playerX] [playerY - 1] == 7) {
					lastCheckPointX = playerX;
					lastCheckPointY = playerY;	
				}

				if (gameBoard [playerX] [playerY] == 100) {
					Destroy (key1);
					Destroy (door1);
					for (int x = 0; x < levelBreite; x++) {
						for (int y = 0; y < levelHoehe; y++) {
							if (gameBoard [x] [y] == 200) {
								gameBoard [x] [y] = 0; 
							}
						}
					}
				}
			

			player.transform.position = new Vector3 (playerX, playerY, 0);		
			gameObject.transform.position = new Vector3 (playerX, playerY, - 17);
		}
		}

	}
}


