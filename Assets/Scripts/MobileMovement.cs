using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour {

	//Enums
	public enum Facing{

		UP,
		RIGHT
	}

	public enum Grid{

		HOLE,
		EMPTY,
	}

	//Public Variables
	public float speed;
	public bool horizontal;
	public Facing crabFace;
	public Grid gridType;

	//Private Variables
	private Rect leftRect;
	private Rect rightRect;
	private Vector3 posToCheck;




	void Start () {

		crabFace = Facing.UP;
		gridType = Grid.EMPTY;
		horizontal = true;
		leftRect = new Rect (0, 0, Screen.width / 2, Screen.height);
		rightRect = new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height);
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Vector3.zero;

		if(Input.GetMouseButtonDown (0)){

			mousePos = Input.mousePosition;
		

			if(rightRect.Contains (Input.mousePosition)){

				Turn (mousePos);
			}

			
		}

		if(Input.GetMouseButtonUp (0) && leftRect.Contains (Input.mousePosition)){

			Vector3 mouseUpPos = Input.mousePosition;

			Vector3 currentPos = new Vector3 (mouseUpPos.x - mousePos.x, mouseUpPos.y - mousePos.y, 0f);
			currentPos.Normalize ();
	
		
			float direction = (currentPos.x > 0f && currentPos.y > -0.5f && currentPos.y < 0.5f) ? -1f : 1f;
		    Debug.Log ("Direction : " + direction);					
			posToCheck =  GetTouchPosition (mousePos,direction);
			MoveToGrid (posToCheck);





		}
		
	}

	Vector3 GetTouchPosition(Vector3 tPos , float direction){

		Vector3 checkPos = transform.position;

	

		checkPos = (horizontal) ? new Vector3 (transform.position.x + direction, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z  + direction);

		return checkPos;


	}

	void Turn(Vector3 tPos){


		if(horizontal){

			transform.rotation = Quaternion.Euler (0f, -90f, 0f);
			crabFace = Facing.RIGHT;
			horizontal = false;

		}else if(!horizontal){
			
			transform.rotation = Quaternion.Euler (0f, 180f, 0f);
			crabFace = Facing.UP;
			horizontal = true;			
		}

	}

	void MoveToGrid(Vector3 movePos){

		transform.position = new Vector3 (Mathf.Clamp (movePos.x,-4f,4f) , transform.position.y, Mathf.Clamp (movePos.z,-3f,2f));

	}
}
