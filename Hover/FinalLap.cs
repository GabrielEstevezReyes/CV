using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLap : MonoBehaviour {
	public GameObject mangr;
	int playerCount;
	GameObject [] players;
	GameObject PlayerF;
	GameObject PlayerS;
	bool lleno;
	// Use this for initialization
	void Start () {
		playerCount = mangr.GetComponent<CamManager> ().players;
		players = GameObject.FindGameObjectsWithTag ("Car");
		if(playerCount == 1){
			PlayerF= players[0];
		}
		if(playerCount == 2){
			PlayerF= players[0];
			PlayerS = players [1];
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject == PlayerF){
			mangr.GetComponent<LapManager> ().CheckEnd (1);
		}
		if(other.gameObject == PlayerS){
			mangr.GetComponent<LapManager> ().CheckEnd (2);
		}

	}
}
