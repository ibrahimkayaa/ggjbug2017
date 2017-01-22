using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



	//Public variables
	public Text eggText;
    public ibrahimCrab PlayerCharacter;
	public GameObject MainMenu;
	public GameObject OptionsMenu;
	public GameObject CreditsMenu;
	public GameObject PauseButton;
	public GameObject PausedMenu;
	public GameObject hollyCrab;

	public bool isGlobal;
	//private Variables
	private int EggeCount;
    [SerializeField] private Transform _latestCheckpoint;


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

		isGlobal = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log ("Eggs : " + EggeCount);
		
	}

    public void UpdateCheckpoint(Transform checkpoint)
    {
        _latestCheckpoint = checkpoint;
    }

    public void OnDeath()
    {
        Debug.Log("Dead");
        //display death anim
        //delayed teleport/move to checkpoint

        PlayerCharacter.ForceMove(_latestCheckpoint.position);

    }

    public static void CollectedEgg()
    {
        Instance.eggText.text = Instance.EggeCount.ToString();
    }

	public void GoToOptionsMenu(){

		MainMenu.SetActive (false);
		OptionsMenu.SetActive (true);
		CreditsMenu.SetActive (false);
	}
	public void GoToCreditsMenu(){
		MainMenu.SetActive (false);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (true);
	}

	public void BackToMainMenu(){
		MainMenu.SetActive (true);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (false);
	}

	public void StartGame(){
		MainMenu.SetActive (false);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (false);
		hollyCrab.SetActive (true);
		PauseButton.SetActive (true);
	}

	public void PauseGame(){

		Time.timeScale = 0f;
		PausedMenu.SetActive (true);
		PauseButton.SetActive (false);
		hollyCrab.GetComponent <ibrahimCrab>().enabled = false;

	}

	public void ResumeGame(){
		Time.timeScale = 1f;
		PausedMenu.SetActive (false);
		PauseButton.SetActive (true);
		hollyCrab.GetComponent <ibrahimCrab>().enabled = true;

	}

	public void ToggleWorldLocal(){

		if(isGlobal){
			PlayerCharacter.normalized = true;
			isGlobal = false;

			Debug.Log ("Normalized");

		}else if(!isGlobal){
			PlayerCharacter.normalized = false;
			isGlobal = true;

			Debug.Log ("Unnormalized");
		}
	}

	public void ExitGame(){

		Application.Quit ();
	}
}
