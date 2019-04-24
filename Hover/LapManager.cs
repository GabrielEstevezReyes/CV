using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour {
	public int FirstL, SecondL;
	// Use this for initialization
	void Start () {
		FirstL = 0;
		SecondL = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckEnd(int plyr){
		if(plyr==1 && FirstL==3){
			Debug.Log ("Gano 1");
			FirstL = 0;
		}
		else if (plyr == 2 && SecondL == 3) {
			Debug.Log ("Gano 2");
			SecondL = 0;
		} 
		else {
			Debug.Log ("Empate");
		}
	}
}
