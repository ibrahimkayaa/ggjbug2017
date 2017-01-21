using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	//Public Variables
	public float speed;


	//Private Variables
	private float horizontalVel;
	private float verticalVel;
	private Rigidbody rb;
	private Transform tr;
	private Vector3 pos;
	private Vector3 lastPos;
	bool horizontal;






	void Start () {


		pos = transform.position;
		tr = transform;

		rb = GetComponent <Rigidbody> ();
	
	}
	

	void Update () {


		GetPlayerInput ();
		
	}




	void GetPlayerInput(){


		if(Input.GetKeyDown (KeyCode.A) && tr.position == pos){
			if(horizontal) {
				pos.z += 1;

			}
			else {
				pos.x -= 1;

			}

			lastPos = pos;


			
		}else if(Input.GetKeyDown (KeyCode.D) && tr.position == pos){
			if(horizontal) {
				pos.z -= 1;

			}
			else {
				pos.x += 1;

			}

			lastPos = pos;
		}

		if(CanMoveSides ()){

			if(Input.GetKeyDown (KeyCode.W) ){


				transform.Rotate (0f, -90f , 0f,Space.World);
				horizontal = !horizontal;


			}else if(Input.GetKeyDown (KeyCode.S)){

				transform.Rotate (0f, 90f, 0f,Space.World);
				horizontal = !horizontal;
			}
			
		}

		/*if(Input.GetKeyDown (KeyCode.W) ){


			transform.Rotate (0f, -90f , 0f,Space.World);
			horizontal = !horizontal;


		}else if(Input.GetKeyDown (KeyCode.S)){

			transform.Rotate (0f, 90f, 0f,Space.World);
			horizontal = !horizontal;
		}*/


		if(CanMoveTowards ())
		{

			transform.position = Vector3.MoveTowards (transform.position, pos ,Time.deltaTime * speed);
		}



		//rb.MovePosition (pos);
		//transform.position = new Vector3(pos.x,transform.position.y,pos.z);
		//lastPos = transform.position;
		//rb.MovePosition (pos);
		//rb.MovePosition (transform.position + new Vector3(0.1f, 0f, 0f));


		
	}

	bool CanMoveSides(){

		RaycastHit hitInfo;

		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		Vector3 bck = transform.TransformDirection (Vector3.back);


		if(Physics.Raycast(transform.position,fwd,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something back of the crab");
				return false;
			}
		}

		if(Physics.Raycast(transform.position,bck,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something in front of the crab");
				return false;
			}
		}
			

		return true;

		
	}


	bool CanMoveTowards(){

		RaycastHit hitInfo;

		//Vector3 fwd = transform.TransformDirection (Vector3.forward);
		//Vector3 bck = transform.TransformDirection (Vector3.back);
		Vector3 right = transform.TransformDirection (Vector3.right);
		Vector3 left = transform.TransformDirection (Vector3.left);

		/*if(Physics.Raycast(transform.position,fwd,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something back of the crab");
				return false;
			}
		}

		if(Physics.Raycast(transform.position,bck,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something in front of the crab");
				return false;
			}
		}*/

		if(Physics.Raycast(transform.position,left,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something on the right of the crab");
				return false;
			}
		}

		if(Physics.Raycast(transform.position,right,out hitInfo,1f)){

			if(hitInfo.collider.tag == "box"){
				Debug.Log ("There is something on the left of the crab");
				return false;
			}
		}

		return true;
	}

	/*void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "box"){
			Debug.Log ("AMK");
			ContactPoint contact = other.contacts [0];
			Vector3 pussy = contact.point.normalized * 2f;
			Debug.Log (pussy);
			transform.position = new Vector3(pussy.x, transform.position.y,transform.position.z);


		}
	}*/



}
