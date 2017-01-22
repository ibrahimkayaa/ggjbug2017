using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



	//Public variables
	public Text eggText;
    public ibrahimCrab PlayerCharacter;

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
}
