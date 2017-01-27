using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibrahimCameraMotor : MonoBehaviour {

	// Use this for initialization
	public GameObject target;
	[Range(1,5)]
	public float trackingSpeed;

	private Vector3 offSet;

	void Start () {

		offSet = transform.position - target.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.x > target.transform.position.x + offSet.x){

			transform.position = Vector3.Lerp (new Vector3(transform.position.x , transform.position.y, transform.position.z ), new Vector3(target.transform.position.x + offSet.x, target.transform.position.y + offSet.y,transform.position.z), Time.deltaTime * trackingSpeed);

		}else{

			transform.position = Vector3.Lerp (new Vector3(transform.position.x , transform.position.y, transform.position.z ), new Vector3(target.transform.position.x + offSet.x, target.transform.position.y + offSet.y,transform.position.z), Time.deltaTime * 1f);

		}


	}
}
