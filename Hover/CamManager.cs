using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {
	
	public GameObject carPlayer;
	public GameObject []laps;
	public GameObject[] player;
	public int players;
    public GameObject target;
	public GameObject fullGM;

	void Start () {

		fullGM = GameObject.FindGameObjectWithTag ("Singleton");
		players = fullGM.GetComponent<FullGM> ().playerCount;
		laps = GameObject.FindGameObjectsWithTag ("Lap");

		for (int i = 0; i < players; i++) {
			//Debug.Log ("Paso");
            Instantiate (carPlayer, target.transform.position, Quaternion.identity);
		}

		player = GameObject.FindGameObjectsWithTag ("Car");
        

		for(int i = 0; i < 4; i++){
			laps [i].GetComponent<Laps> ().players = player;
		}

		switch (players) {
		case(1):
			player [0].GetComponentInChildren<Camera> ().rect = new Rect (0, 0, 1, 1);

			break;
		case(2):
			
			player [0].GetComponentInChildren<Camera> ().rect = new Rect (0, 0.5f, 1, 0.5f);
			player [1].GetComponentInChildren<Camera> ().rect = new Rect (0, 0, 1, 0.5f);

			RectTransform[] uiComponents;

			for (int i = 0; i < 2; i++) {
				uiComponents = player [i].GetComponentInChildren<Canvas> ().GetComponentsInChildren<RectTransform> ();
				uiComponents [1].anchoredPosition = new Vector2 (475,-85);
				uiComponents [2].anchoredPosition = new Vector2 (-377,71);
				uiComponents [3].anchoredPosition = new Vector2 (299, -107);
				uiComponents [4].anchoredPosition = new Vector2 (300, -108);
				uiComponents [5].anchoredPosition = new Vector2 (302, -80);
				uiComponents [6].anchoredPosition = new Vector2 (-354, -111);
				Debug.Log ("Paso" + i.ToString());
			}
			break;
		default:
			Debug.Log ("Error reading players, OUT OF RANGE");
			break;
		}
	}
	void Update () {
		
	}
}
