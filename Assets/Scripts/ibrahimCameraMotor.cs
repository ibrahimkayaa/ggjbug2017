using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibrahimCameraMotor : MonoBehaviour {

	// Use this for initialization
	public GameObject target;

	private Vector3 offSet;

	void Start () {

		offSet = transform.position - target.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Slerp (new Vector3(transform.position.x , transform.position.y, transform.position.z ), new Vector3(target.transform.position.x + offSet.x, target.transform.position.y + offSet.y,transform.position.z), Time.deltaTime * 0.75f);
		
	}
}
