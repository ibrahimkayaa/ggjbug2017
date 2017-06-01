using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	// Use this for initialization
	public bool timeTodestroy;
	void Start () {

		timeTodestroy = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(timeTodestroy){

			StartCoroutine (DestroyAfterASecond ());
		}
	}

	IEnumerator DestroyAfterASecond(){


	

		GameManagerController.Instance.nodesArray.Remove (this.gameObject);
		yield return new WaitForSeconds (0.25f);
		transform.position = new Vector3 (transform.position.x, transform.position.y - 8f * Time.deltaTime, transform.position.z);
		StartCoroutine (EnableAfterMotion ());

		



	}

	IEnumerator EnableAfterMotion(){
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);
	}

	void OnDisable(){

		StopAllCoroutines ();

	}
}
