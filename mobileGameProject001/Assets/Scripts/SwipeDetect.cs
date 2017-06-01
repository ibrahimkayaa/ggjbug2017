using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Security.Policy;

public class SwipeDetect : MonoBehaviour {

	public static SwipeDetect Instance{get;set;}

	public enum SwipeDirection{

		NONE,
		UP,
		DOWN,
		RIGHT,
		LEFT,
		TAP
	}

	//Public Variables
	public SwipeDirection sDirection;
	public float minSwipeDistance;

	//Private Variables
	Rect leftRect;
	Rect rightRect;
	Vector2 startPos;
	Vector2 endPos;

	void Awake(){

		if(Instance !=null && Instance !=this){

			Destroy (gameObject);

		}else{

			Instance = this;

		}
			
	}

	void Start () {

		sDirection = SwipeDirection.NONE;

		leftRect = new Rect (0, 0, Screen.width / 2, Screen.height);

		rightRect = new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {

			Touch touch = Input.touches [0];

			if (touch.phase == TouchPhase.Began) {

				startPos = new Vector2 (touch.position.x, touch.position.y);

			}

			if (touch.phase == TouchPhase.Ended) {

				endPos = new Vector2 (touch.position.x, touch.position.y);	

				Vector3 deltaPos = new Vector3 (endPos.x - startPos.x, endPos.y - startPos.y);

				if (deltaPos.magnitude < minSwipeDistance) {

					sDirection = SwipeDirection.NONE;

					return;
				}

				deltaPos.Normalize ();

				if (deltaPos.x > 0 && deltaPos.y > -0.5f && deltaPos.y < 0.5f) {

					sDirection = SwipeDirection.RIGHT;

				} else if (deltaPos.x < 0 && deltaPos.y > -0.5f && deltaPos.y < 0.5f) {

					sDirection = SwipeDirection.LEFT;
				} else if (deltaPos.x > -0.5f && deltaPos.x < 0.5f && deltaPos.y > 0) {

					sDirection = SwipeDirection.UP;
				} else if (deltaPos.x > -0.5f && deltaPos.x < 0.5f && deltaPos.y < 0) {

					sDirection = SwipeDirection.DOWN;
				}

			}

/*			if(leftRect.Contains (touch.position)){

				if(touch.phase == TouchPhase.Began){

					startPos = new Vector2 (touch.position.x, touch.position.y);

				}

				if(touch.phase == TouchPhase.Ended){

					endPos = new Vector2 (touch.position.x, touch.position.y);	

					Vector3 deltaPos = new Vector3 (endPos.x - startPos.x, endPos.y - startPos.y);

					if(deltaPos.magnitude < minSwipeDistance){
						
						sDirection = SwipeDirection.NONE;

						return;
					}

					deltaPos.Normalize ();

					if(deltaPos.x > 0 && deltaPos.y > -0.5f && deltaPos.y < 0.5f){
						
						sDirection = SwipeDirection.RIGHT;

					}else if( deltaPos.x < 0 && deltaPos.y > -0.5f && deltaPos.y < 0.5f){
						
						sDirection = SwipeDirection.LEFT;
					}else if( deltaPos.x > -0.5f && deltaPos.x < 0.5f && deltaPos.y > 0) {

						sDirection = SwipeDirection.UP;
					}else if ( deltaPos.x > - 0.5f && deltaPos.x < 0.5f && deltaPos.y < 0){

						sDirection = SwipeDirection.DOWN;
					}

				}
					
				
			}

		if(rightRect.Contains (touch.position)){

				switch(touch.phase){

				case TouchPhase.Began:

					sDirection = SwipeDirection.TAP;

					break;

				case TouchPhase.Ended:
					
					sDirection = SwipeDirection.NONE;

					break;

				default:
					
					sDirection = SwipeDirection.NONE;
					break;

				}
					
				Debug.Log (sDirection);
				
			}
		}else{

			sDirection = SwipeDirection.NONE;
		}*/

		}
	}


}
