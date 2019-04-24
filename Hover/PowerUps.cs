using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour {
	
	int control = 1;
	public GameObject clone;
    public GameObject bull;
	//public GameObject padre;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)){
			if(control == 1){
			//Clone ();
				Instantiate (clone, new Vector3(gameObject.transform.position.x+5, gameObject.transform.position.y, gameObject.transform.position.z), this.transform.rotation);
			control-=1;
			}
		}

        if (Input.GetKeyDown(KeyCode.T))
        {
                Instantiate(bull, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+2), this.transform.rotation);
         
        }

    }

	void OnTriggerEnter(Collider other){
		if(other.CompareTag ("Cloner")){
			Instantiate (clone, gameObject.transform.position, Quaternion.identity);
			//Clone ();
			Destroy (other.gameObject);
			Debug.Log ("Funciona");
		}
	}

	void Clone(){
		
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.up);
		if (Physics.Raycast(ray, out hit))
		{
			Instantiate (clone, gameObject.transform.position, Quaternion.identity);
		}
	}
}

