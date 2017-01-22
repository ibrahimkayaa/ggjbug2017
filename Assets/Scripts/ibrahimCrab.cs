﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibrahimCrab : MonoBehaviour {

	//public variables
	public float speed;
	public bool normalized;

    public AudioSource AS;

    public AudioClip BottleHit;
    public AudioClip EggPickup;
    public AudioClip SandWalk;
    public AudioClip StoneObs;
    public AudioClip WoodObs;



    //Private Variables
    [SerializeField]
    private Animator _animator;
	private float horizontalVel;
	private float verticalVel;
	private Rigidbody rb;
	private Transform tr;
	private Vector3 pos;
	bool horizontal;
	private Vector3 posToCheck;
    private int inputBlocked = 0;

	public enum EyeSight{
		DOWN,
		RIGHT,
		UP,
		LEFT
	}

	public enum GridType{
		HOLE,
		OTHER,

	}

	private EyeSight eye;
	private GridType gType;

	// Use this for initialization
	void Start () {
        if (_animator == null) Debug.LogError("animator attachle");
	
		horizontal = true;
		rb = GetComponent <Rigidbody> ();
		tr = transform;
		pos = transform.position;
		eye = EyeSight.DOWN;
		gType = GridType.OTHER;


	}
	
	// Update is called once per frame
	void Update () {

        if (inputBlocked>0) return;

        if (Input.GetButtonDown("Backward"))
        {
            //Debug.Log ("Crab position : " +transform.position);

            RaycastHit hitInfo;


            if (!normalized)
            {

                if (eye == EyeSight.DOWN || eye == EyeSight.LEFT)
                {

                    posToCheck = (horizontal) ? new Vector3(transform.position.x + 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + 1f);
                    Debug.Log("Position to Check : " + posToCheck);
                }
                else if (eye == EyeSight.UP || eye == EyeSight.RIGHT)
                {

                    posToCheck = (horizontal) ? new Vector3(transform.position.x - 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 1f);
                    Debug.Log("Position to Check : " + posToCheck);
                }
            }
            else
            {

                posToCheck = (horizontal) ? new Vector3(transform.position.x - 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + 1f);
                //Debug.Log ("Position to Check : " + posToCheck);

            }


            if (Physics.Raycast(posToCheck, Vector3.up, out hitInfo, 1.5f))
            {

                if (hitInfo.collider.GetComponent<BoxCollider>())
                {

                    //Debug.Log(hitInfo.collider.tag);
                    CheckColliderTags(hitInfo, posToCheck);

                }

            }
            else
            {


                //Debug.Log ("Nothing");


                MoveToTheGrid(posToCheck);
                //transform.position = new Vector3 (Mathf.Clamp (posToCheck.x,-4f,120f) , transform.position.y, Mathf.Clamp (posToCheck.z,-3f,2f));

            }

            //Debug.Log ("out out amk");

        }
        else if (Input.GetButtonDown("Forward"))
        {

            //Debug.Log("Crab position : " + transform.position);

            RaycastHit hitInfo;

            if (!normalized)
            {

                if (eye == EyeSight.DOWN || eye == EyeSight.LEFT)
                {

                    posToCheck = (horizontal) ? new Vector3(transform.position.x - 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 1f);
                    Debug.Log("Position to Check : " + posToCheck);
                }
                else if (eye == EyeSight.UP || eye == EyeSight.RIGHT)
                {

                    posToCheck = (horizontal) ? new Vector3(transform.position.x + 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + 1f);
                    Debug.Log("Position to Check : " + posToCheck);

                }

            }
            else
            {
                posToCheck = (horizontal) ? new Vector3(transform.position.x + 1f, transform.position.y - 1f, transform.position.z) : new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 1f);
                //Debug.Log("Position to Check : " + posToCheck);


            }



            if (Physics.Raycast(posToCheck, Vector3.up, out hitInfo, 1.5f))
            {

                if (hitInfo.collider.GetComponent<BoxCollider>())
                {


                    //Debug.Log(hitInfo.collider.tag);
                    CheckColliderTags(hitInfo, posToCheck);


                    //Debug.Log (hitInfo.collider.tag);
                    //CheckColliderTags (hitInfo,posToCheck);

                }



            }
            else
            {

                //Debug.Log("Nothing");
                MoveToTheGrid(posToCheck);                
                //transform.position = new Vector3 (Mathf.Clamp (posToCheck.x,-4f,120f), transform.position.y, Mathf.Clamp ( posToCheck.z, -3f,2f));

            }
        }

		/*if(gType != GridType.HOLE){


			Turn ();
		}else if((gType == GridType.HOLE && eye == EyeSight.LEFT) || (gType == GridType.HOLE && eye ==  EyeSight.RIGHT)){

			//horizontal = !horizontal;
			Turn ();
			
		}*/

		Turn ();


	}

    public void ForceMove(Vector3 pos)
    {
        //MoveToTheGrid(pos);
        StopCoroutine("MoveTowards");
        transform.position = pos;
    }

    IEnumerator DelayedInputResume()
    {
        inputBlocked++;
        yield return new WaitForSeconds(0.3f);
        inputBlocked--;

    }

	void Turn(){

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

			//Debug.Log (eye);

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

	void MoveToTheGrid(Vector3 posToMove){

		if(gType == GridType.HOLE){
			gType = GridType.OTHER;
			gameObject.GetComponent <BoxCollider>().enabled = true;
		}
        else _animator.ResetTrigger("cower");
        _animator.SetTrigger("bombo");
        //transform.position = new Vector3 (Mathf.Clamp (posToMove.x,-4f,120f), transform.position.y, Mathf.Clamp ( posToMove.z, -3f,2f));
        StartCoroutine(MoveTowards(posToMove));
	}

    IEnumerator MoveTowards(Vector3 posToMove)
    {
        //disable inputs
        inputBlocked++;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(posToMove.x, -4f, 120f), transform.position.y, Mathf.Clamp(posToMove.z, -3f, 2f));
        Vector3 direction = (clampedPos - transform.position ).normalized;
        //Debug.DrawLine(transform.position, clampedPos, Color.red, 2f);
        //Debug.Log(Vector3.Distance(transform.position, clampedPos));
        while (Vector3.Distance(transform.position, clampedPos) > .1f)
        {
            //Debug.Log(Vector3.Distance(transform.position, clampedPos));
            Vector3.Distance(transform.position, clampedPos);            
            transform.position = (direction*Time.deltaTime * 2f + transform.position);
            yield return new WaitForEndOfFrame();
        }
        transform.position = clampedPos;
        //enable inputs
        inputBlocked--;
    }


	void CheckColliderTags(RaycastHit hit, Vector3 posToMove){
        switch (hit.collider.tag)
        {

            case "Trap":

                Debug.Log("It is a trap");
                Died();                
                break;
            case "Hole":

                Debug.Log("It is a hole");
                MoveToTheGrid(posToMove);
                _animator.SetTrigger("cower");
                StartCoroutine(DelayedInputResume());                
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gType = GridType.HOLE;
                


                break;

            case "Egg":                
                Debug.Log("It is an Egg");
                MoveToTheGrid(posToMove);
                hit.collider.gameObject.SetActive(false);
                GameManager.Instance.eggCount++;
                GameManager.CollectedEgg();
                Destroy(hit.collider.gameObject);
                AS.PlayOneShot(EggPickup);
                break;


            case "Checkpoint":
            case "checkpoint":
                Debug.Log("Checkpoint saved");
                MoveToTheGrid(posToMove);
                
                break;
            case "Rock":
                AS.PlayOneShot(StoneObs);
                break;
            case "Ship":
                AS.PlayOneShot(WoodObs);
                break;
            default:
                
                break;

        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        switch(other.tag)
        {
            case "Wave":
            case "wave":
                Died();
                break;
            case "checkpoint":
            case "Checkpoint":
                GameManager.Instance.UpdateCheckpoint(other.transform);
                break;
        }
    }


    private void Died()
    {
        Debug.Log("died");

        //add delay
        GameManager.Instance.OnDeath();
    }
}
