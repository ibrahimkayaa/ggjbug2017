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

		if(Input.GetKeyDown (KeyCode.W) ){


			transform.Rotate (0f, -90f , 0f,Space.World);
			horizontal = !horizontal;


		}else if(Input.GetKeyDown (KeyCode.S)){

			transform.Rotate (0f, 90f, 0f,Space.World);
			horizontal = !horizontal;
		}


	
		transform.position =Vector3.MoveTowards (transform.position, pos ,Time.deltaTime * speed);
		//rb.MovePosition (pos);
		//transform.position = new Vector3(pos.x,transform.position.y,pos.z);
		//lastPos = transform.position;
		//rb.MovePosition (pos);
		//rb.MovePosition (transform.position + new Vector3(0.1f, 0f, 0f));


		
	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "box"){
			Debug.Log ("AMK");
			ContactPoint contact = other.contacts [0];
			Vector3 pussy = contact.point.normalized * 2f;
			Debug.Log (pussy);
			transform.position = new Vector3(pussy.x, transform.position.y,transform.position.z);


		}
	}



}
