using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	//Public variables
	public Text eggText;

	//private Variables
	private int EggeCount;


	public int eggCount {

		get{

			return EggeCount;
		}
		set{

			EggeCount = value;
			
		}
	}

	public static GameManager Instance{ get; set;}

	void Awake(){

		if(Instance !=null && Instance !=this){

			GameManager.Instance.eggText = this.eggText;

			Destroy (gameObject);
			
		}else{

			Instance = this;
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Eggs : " + EggeCount);
		eggText.text = EggeCount.ToString ();
	}
}
