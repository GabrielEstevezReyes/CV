using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GlobalManager : MonoBehaviour {

	public GameObject main;
	public GameObject levels;
	private GameObject fullGM;
 
	// Use this for initialization
	void Start () {
		fullGM = GameObject.FindGameObjectWithTag ("Singleton");
	}
	
	// Update is called once per frame
	void Update () {
       
	}

	public void StartGame(int players){
		fullGM.GetComponent<FullGM> ().playerCount = players;
		SceneManager.LoadScene ("MainScene");
	}

	public void SceneSelect(string subScene){

		if(subScene == "main"){
			levels.SetActive(false);
			main.SetActive(true);
		}
		if (subScene == "levels") {
			main.SetActive (false);
			levels.SetActive (true);
		}
	}

	public void QuitGame(){
		Application.Quit ();	
	}
}
