using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibrahimCrab : MonoBehaviour {

	//public variables
	public float speed;
	public bool normalized;


	//Private Variables
	private float horizontalVel;
	private float verticalVel;
	private Rigidbody rb;
	private Transform tr;
	private Vector3 pos;
	bool horizontal;
	private Vector3 posToCheck;

	public enum EyeSight{
		DOWN,
		RIGHT,
		UP,
		LEFT
	}

	private EyeSight eye;

	// Use this for initialization
	void Start () {

	
		horizontal = true;
		rb = GetComponent <Rigidbody> ();
		tr = transform;
		pos = transform.position;
		eye = EyeSight.DOWN;


	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Backward")) {
			Debug.Log ("Crab position : " +transform.position);

			RaycastHit hitInfo;


			if(!normalized){

				if(eye == EyeSight.DOWN || eye == EyeSight.LEFT){

					posToCheck = (horizontal) ? new Vector3 (transform.position.x + 1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z +1f);
					Debug.Log ("Position to Check : " + posToCheck);
				}else if( eye == EyeSight.UP  || eye == EyeSight.RIGHT){

					posToCheck = (horizontal) ? new Vector3 (transform.position.x - 1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z -1f);
					Debug.Log ("Position to Check : " + posToCheck);
				}
			}else{

				posToCheck = (horizontal) ? new Vector3 (transform.position.x - 1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z +1f);
			Debug.Log ("Position to Check : " + posToCheck);
				
			}
				

			if(Physics.Raycast(posToCheck,Vector3.up,out hitInfo,1.5f)){

				if(hitInfo.collider.GetComponent <BoxCollider>()){



				}

			}else{

				Debug.Log ("Nothing");



				transform.position = new Vector3 (Mathf.Clamp (posToCheck.x,-4f,20f) , transform.position.y, Mathf.Clamp (posToCheck.z,-3f,2f));
		
			}

			Debug.Log ("out out amk");
			
		}else if(Input.GetButtonDown ("Forward")){

			Debug.Log ("Crab position : " +transform.position);

			RaycastHit hitInfo;

			if(!normalized){

				if(eye == EyeSight.DOWN || eye == EyeSight.LEFT){

					posToCheck = (horizontal) ? new Vector3 (transform.position.x -1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z -1f);
					Debug.Log ("Position to Check : " + posToCheck);
				}else if(eye == EyeSight.UP || eye == EyeSight.RIGHT){

					posToCheck = (horizontal) ? new Vector3 (transform.position.x +1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z +1f);
					Debug.Log ("Position to Check : " + posToCheck);

				}

			}else{
				posToCheck = (horizontal) ? new Vector3 (transform.position.x +1f, transform.position.y - 1f, transform.position.z) : new Vector3 (transform.position.x , transform.position.y - 1f, transform.position.z -1f);
				Debug.Log ("Position to Check : " + posToCheck);

				
			}



			if(Physics.Raycast(posToCheck,Vector3.up,out hitInfo,1.5f)){

				if(hitInfo.collider.GetComponent <BoxCollider>()){

					Debug.Log (hitInfo.collider.tag);
					CheckColliderTags (hitInfo);

				}



			}else{

				Debug.Log ("Nothing");
				transform.position = new Vector3 (Mathf.Clamp (posToCheck.x,-4f,20f), transform.position.y, Mathf.Clamp ( posToCheck.z, -3f,2f));

			}
		}


		if (Input.GetButtonDown ("TurnLeft")) {

			transform.Rotate (0f,-90f,0f,Space.World);
			horizontal = !horizontal;

			if(eye == EyeSight.DOWN){
				eye = EyeSight.LEFT;
			}else if(eye == EyeSight.LEFT){
				eye = EyeSight.UP;
			}else if(eye == EyeSight.UP){
				eye = EyeSight.RIGHT;
			}else if( eye == EyeSight.RIGHT){
				eye = EyeSight.DOWN;
			}

			Debug.Log (eye);
			
		}else if(Input.GetButtonDown ("TurnRight")){
			
			transform.Rotate (0f,90f,0f,Space.World);
			horizontal = !horizontal;

			if(eye == EyeSight.DOWN){
				eye = EyeSight.RIGHT;
			}else if(eye == EyeSight.RIGHT){
				eye = EyeSight.UP;
			}else if(eye == EyeSight.UP){
				eye = EyeSight.LEFT;
			}else if( eye == EyeSight.LEFT){
				eye = EyeSight.DOWN;
			}
			Debug.Log (eye);
			
		}
	}

	void CheckColliderTags(RaycastHit hit){
		switch(hit.collider.tag){

		case "Trap":

			Debug.Log ("It is a trap");

			break;
		case "Hole":

			Debug.Log ("It is a hole");


			break;
	
		default:
			break;

		}
	}
}
