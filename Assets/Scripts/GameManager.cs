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
	public GameObject howToMenu;
	public GameObject hollyCrab;
	public GameObject eggHUD;
	public GameObject lifeHUD;
	public GameObject endScene;
	public GameObject badEndScene;
	public GameObject[] crableHealthIconArray;
	public AudioClip inGameAudio;
	public AudioClip inMenuAudio;


	public bool isGlobal;

	//private Variables
	private int EggeCount;
	[HideInInspector]public int CrableHealth;
    [SerializeField] private Transform _latestCheckpoint;

	private bool isMute;
	[SerializeField]
	public List<GameObject> eggsList = new List<GameObject>();
	private Transform startPos;
	private AudioSource mainSoundSource;


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
		isMute = false;
		startPos = _latestCheckpoint;
		CrableHealth = 5;
		mainSoundSource = GetComponent <AudioSource> ();
	
		for(int i = 0 ; i < GameObject.FindGameObjectsWithTag ("Egg").Length ; i++){

			GameObject tempObj = GameObject.FindGameObjectsWithTag ("Egg").GetValue (i) as GameObject;

			eggsList.Add (tempObj);
		}

		
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log ("Eggs : " + EggeCount);
		SubstractHealth ();
		
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
        PlayerCharacter.died = false;
    }

    public static void CollectedEgg()
    {
        Instance.eggText.text = Instance.EggeCount.ToString();
    }

	public void GoToOptionsMenu(){

		MainMenu.SetActive (false);
		OptionsMenu.SetActive (true);
		CreditsMenu.SetActive (false);
		howToMenu.SetActive (false);
	}
	public void GoToCreditsMenu(){
		MainMenu.SetActive (false);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (true);
		howToMenu.SetActive (false);
	}

	public void GoToHowToMenu(){

		MainMenu.SetActive (false);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (false);
		howToMenu.SetActive (true);
	}

	public void BackToMainMenu(){
		MainMenu.SetActive (true);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (false);
		howToMenu.SetActive (false);
	}

	public void StartGame(){
		MainMenu.SetActive (false);
		OptionsMenu.SetActive (false);
		CreditsMenu.SetActive (false);
		howToMenu.SetActive (false);
		hollyCrab.SetActive (true);
		PauseButton.SetActive (true);
		eggHUD.SetActive (true);
		lifeHUD.SetActive (true);
		mainSoundSource.clip = inGameAudio;
		mainSoundSource.volume = 0.35f;
		mainSoundSource.PlayDelayed (0.3f);

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

	public void GoToMainMenu(){

		Time.timeScale = 1f;
		PausedMenu.SetActive (false);
		MainMenu.SetActive (true);
		Instance.EggeCount = 0;
		eggHUD.SetActive (false);
		lifeHUD.SetActive (false);
		CollectedEgg ();
		ResetEggs ();
		hollyCrab.GetComponent <ibrahimCrab>().enabled = true;
		hollyCrab.SetActive (false);
		PlayerCharacter.ForceMove(startPos.position);
		mainSoundSource.clip = inMenuAudio;
		mainSoundSource.volume = 0.75f;
		mainSoundSource.PlayDelayed (0.2f);


	}

	public void RestartGame(){

		Time.timeScale = 1f;
		PausedMenu.SetActive (false);
		Instance.EggeCount = 0;
		Instance.CrableHealth = 5;
		CollectedEgg ();
		ResetEggs ();
		PauseButton.SetActive (true);
		PlayerCharacter.ForceMove (startPos.position);
		StartCoroutine (LateEnabled ());
	}

	IEnumerator LateEnabled(){

		yield return new WaitForSeconds (2f);
		hollyCrab.GetComponent <ibrahimCrab>().enabled = true;

	}

	/*public void ResetGame(){

		MainMenu.SetActive (true);
		Instance.EggeCount = 0;
		CollectedEgg ();
		ResetEggs ();
		Instance.CrableHealth = 5;

	}*/

	void ResetEggs(){

		for(int i = 0 ; i < eggsList.Count ; i ++){

			eggsList[i].SetActive (true);
		}
	}

	public void SubstractHealth(){

		Color halfColor = Color.black;
		halfColor.a = 0.25f;
		Color fullColor = Color.white;


		for(int i = 0 ; i < crableHealthIconArray.Length ; i++){

			if(CrableHealth > i){

		
				crableHealthIconArray [i].GetComponent <Image> ().color = fullColor;


			}else{

				crableHealthIconArray [i].GetComponent <Image> ().color = halfColor;
				
			}
			
		}
	}

	public void ShowEndScene(){

		endScene.SetActive (true);
	}

	public void CloseEndScene(){

		endScene.SetActive (false);
	}

	public void ShowBadEndScene(){
		
		badEndScene.SetActive (true);
		
	}

	public void Mute(){

		if(!isMute){
			isMute = true;
			AudioListener.pause = true;
		}else if(isMute){
			isMute = false;
			AudioListener.pause = false;
		}
	}
}
