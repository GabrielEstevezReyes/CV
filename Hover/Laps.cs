using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laps : MonoBehaviour {
	public GameObject manger;
	GameObject singl;
	public int playerCount;
	public int tipeLap;
	public GameObject [] players;
	public GameObject PlayerF;
	public GameObject PlayerS;

	// Use this for initialization
	void Start () {
		singl = GameObject.FindGameObjectWithTag ("Singleton");
		playerCount = singl.GetComponent<FullGM> ().playerCount;

		//players = GameObject.FindGameObjectsWithTag ("Car");
	}
	
	// Update is called once per frame
	void Update () {
//		if (!lleno){
//				players = GameObject.FindGameObjectsWithTag ("Car");
//			if(players.GetLength == 1){
//				PlayerF= players[0];
//			}
//			if(players.GetLength == 2){
//				PlayerF= players[0];
//				PlayerS = players [1];
//			}
//			lleno = true;
//		}
	}

	void OnTriggerEnter(Collider other){
		if(tipeLap ==1){
			if(playerCount ==1){
				if(other.gameObject == players[0]){
					manger.GetComponent<LapManager> ().FirstL += 1;
				}

			}
			if (playerCount == 2) {
				if(other.gameObject == players[0]){
					manger.GetComponent<LapManager> ().FirstL += 1;
				}
				if(other.gameObject == players[1]){
					manger.GetComponent<LapManager> ().SecondL += 1;
				}
			}

		}
		if(tipeLap ==2){

			if(playerCount ==1){
				if(other.gameObject == players[0]){
					manger.GetComponent<LapManager> ().CheckEnd (1);
				}

			}
			if (playerCount == 2) {
				if(other.gameObject == players[0]){
					manger.GetComponent<LapManager> ().CheckEnd (1);
				}
				if(other.gameObject == players[1]){
					manger.GetComponent<LapManager> ().CheckEnd (2);
				}
			}

		}
	
	}
}
