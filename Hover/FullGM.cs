using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGM : MonoBehaviour {

	public GameObject Myself;
	public int playerCount;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (Myself);
	}
}
