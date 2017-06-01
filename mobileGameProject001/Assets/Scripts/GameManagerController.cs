using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour {


	public List<GameObject> nodesArray = new List<GameObject>();

	public static GameManagerController Instance { get; set;}


	void Awake(){

		if(Instance != null && Instance != this){

			Destroy (gameObject);

		}else{

			Instance = this;
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start () {

		for(int i = 0 ; i < GameObject.FindGameObjectsWithTag ("Node").Length ; i++){

			GameObject tempObject = GameObject.FindGameObjectsWithTag ("Node").GetValue (i) as GameObject;
			nodesArray.Add (tempObject);
		}

		Debug.Log (nodesArray.Count);

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.E)){

			SceneManager.LoadScene (0);

		}
		
	}
}
