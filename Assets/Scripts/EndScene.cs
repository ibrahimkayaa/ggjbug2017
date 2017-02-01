using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {




	public void MainMenu(){

		GameManager.Instance.GoToMainMenu ();
	}

	public void ClosePanel(){

		gameObject.SetActive (false);

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
