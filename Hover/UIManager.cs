using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image speedBar;
	public Text speedText;
	public Text timeInGame;
	public int time;

	Vector3 p1;
	Vector3 p2;
	Vector3 p3;

	int seconds;
	int milisecs;
	int minutes;
	public GameObject player;
	public Vector3 velocity;

	// Use this for initialization
	void Awake () {
		StartCoroutine ("StartTimer");
		p1 = player.transform.position;
		p2 = player.transform.position;

	}

	void FixedUpdate(){
		
		p2 = p1;
		p1 = player.transform.position;

		velocity = (p1 - p2) / Time.deltaTime;

		speedText.text = ((int)velocity.magnitude).ToString ();

		speedBar.fillAmount = velocity.magnitude / 350;

		timeInGame.text = minutes.ToString () + " : " + seconds.ToString () + " : " + milisecs.ToString ();
	}
	

	public IEnumerator StartTimer () {
		while (true) {
			yield return new WaitForSeconds (0.001f);
			time += 1;
			milisecs = (time % 100);
			seconds = (time / 100 ) % 60;
			minutes = (time / 6000);
		}
	}
}
