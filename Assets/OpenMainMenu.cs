using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainMenu : MonoBehaviour {

	public GameObject mainMenuObject;

	public void IntroEnding(){

		gameObject.SetActive (false);
		mainMenuObject.SetActive (true);

	}

	void Update(){

		if(Input.GetKeyDown (KeyCode.Space)){

			IntroEnding ();
		}
	}
}
