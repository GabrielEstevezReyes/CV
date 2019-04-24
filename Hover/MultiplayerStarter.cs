	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerStarter : MonoBehaviour {

	public int players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			players = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			players = 2;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			players = 3;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			players = 4;
		}
	}
}
