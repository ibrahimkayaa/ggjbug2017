using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementClass : MonoBehaviour {


	//Public Variables
	[Range(0,1)]
	public float yDistance;
	public int gridMinMax;
	public GameObject parentObject;

	//private variables
	private float horizontalVel;
	private float verticalVel;
	private Vector3 posToCheck;


	//rotation variables
	public float rotationPeriod = 0.3f;
	public float sideLength = 1f;
	bool isRotate = false;
	bool canMove;
	float directionX = 0f;
	float directionZ = 0f;
	private Rigidbody cubeRB;
	Vector3 startPosition;
	float rotationTime = 0f;
	float radius;
	Quaternion fromRotation;
	Quaternion toRotation;



	// Use this for initialization
	void Start () {

		radius = sideLength * Mathf.Sqrt (2f) / 2f;
		cubeRB = GetComponent <Rigidbody> ();
		//cubeRB.useGravity = false;
		canMove = true;
		SwipeDetect.Instance.sDirection = SwipeDetect.SwipeDirection.NONE;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(gameObject.transform.position.y < 0.25f){

			canMove = false;
		}

		if(gameObject.transform.position.y < -2f){

			gameObject.SetActive (false);
		}

		if (Input.GetButtonDown ("Horizontal")) {

		

			horizontalVel = Input.GetAxisRaw ("Horizontal") *-1f;
		
			posToCheck = new Vector3 (transform.position.x , transform.position.y,transform.position.z + horizontalVel);






		}else if(Input.GetButtonDown ("Vertical")){
			
			verticalVel = Input.GetAxisRaw ("Vertical") * -1f;


			posToCheck = new Vector3 (transform.position.x - verticalVel, transform.position.y, transform.position.z );



		
		}


		if(SwipeDetect.Instance.sDirection == SwipeDetect.SwipeDirection.RIGHT){

			Debug.Log ("RIGHT");
			horizontalVel = -1f;
			
		}else if(SwipeDetect.Instance.sDirection == SwipeDetect.SwipeDirection.LEFT){

			Debug.Log ("LEFT");
			horizontalVel = 1f;
			
		}else if(SwipeDetect.Instance.sDirection == SwipeDetect.SwipeDirection.UP){

			Debug.Log ("UP");
			verticalVel = -1f;
			
		}else if(SwipeDetect.Instance.sDirection == SwipeDetect.SwipeDirection.DOWN){

			Debug.Log ("DOWN");
			verticalVel = 1f;
			
		}

		if(Input.GetButtonUp ("Horizontal") || Input.GetButtonUp ("Vertical")){

			transform.parent = null;
			horizontalVel = 0f;
			verticalVel = 0f;
		}

		if((horizontalVel !=0f || verticalVel != 0f ) && !isRotate  ){

			directionX = verticalVel;
			directionZ = horizontalVel;
			startPosition = transform.position;
			fromRotation = transform.rotation;
			transform.Rotate (directionZ * 90f , 0f, directionX * 90f , Space.World);
			toRotation = transform.rotation;
			rotationTime = 0f;
			isRotate = true;

		}



	}

	void FixedUpdate(){



		if(isRotate && canMove ){

			RaycastHit hitInfo;
			if(Physics.Raycast (/*posToCheck*/transform.position,Vector3.down, out hitInfo,0.5f)){

				Debug.Log ("We hit Something  at position : " + hitInfo.transform.position);

				if(hitInfo.collider.GetComponent <BoxCollider>()){

					Debug.Log ("Has Box Collider");
					hitInfo.transform.gameObject.GetComponent <Destroyable>().timeTodestroy = true;
					//hitInfo.transform.gameObject.GetComponent <Renderer>().material = gameObject.GetComponent <Renderer>().material;
					StartCoroutine (PaintTheCube (hitInfo.transform.gameObject));


				}

			}else{
				Debug.Log ("Empty Space");
				cubeRB.useGravity = true;

			}



			MoveToGrids ();
		}
	}

	IEnumerator PaintTheCube(GameObject obj){

		yield return new WaitForSeconds (0.01f);
		obj.GetComponent <Renderer>().material = gameObject.GetComponent <Renderer>().material;

		
	}

	void OnDisable(){

		Debug.Log (GameManagerController.Instance.nodesArray.Count + " Nodes remain..");
	}
		

	void MoveToGrids(){

		//transform.position = new Vector3 (Mathf.Clamp (posToMove.x,-gridMinMax,gridMinMax) , transform.position.y, Mathf.Clamp (posToMove.z,-gridMinMax,gridMinMax) );
		rotationTime += Time.fixedDeltaTime;
		float ratio = Mathf.Lerp (0f, 1f, rotationTime / rotationPeriod);
		float theTaRad = Mathf.Lerp (0f, Mathf.PI / 2f, ratio);
		float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + theTaRad));
		float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + theTaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));
		float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + theTaRad));	
		transform.position = new Vector3( Mathf.Clamp (startPosition.x + distanceX,-gridMinMax,gridMinMax) , startPosition.y + distanceY, Mathf.Clamp (startPosition.z + distanceZ,-gridMinMax,gridMinMax) );


		transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);

		if(ratio == 1 ){
			SwipeDetect.Instance.sDirection = SwipeDetect.SwipeDirection.NONE;
			isRotate = false;
			directionX = 0;
			directionZ = 0;
			rotationTime = 0;
			horizontalVel = 0f;
			verticalVel = 0f;
		}
	
	}
}
