using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaker : MonoBehaviour {

	public Vector3 levelSize;

	public Transform cubePrefab;
	[Range(0,1)]
	public float outline;
	public Material targetMaterial;

	Vector3 CoordToPosition(float x, float z){

		return new Vector3 (-levelSize.x / levelSize.x   + x, -1.50f, -levelSize.z / levelSize.z  + z);
	}

	void Awake () {
		CreateLevel ();
		
	}
	
	public void CreateLevel(){

		string holderName = "LevelHolder";
		Transform levelHolder = new GameObject (holderName).transform;
		levelHolder.parent = transform;

		int randomX = Random.Range ((int)-1f, (int)levelSize.x-2);
		int randomZ = Random.Range ((int)-1f, (int)levelSize.z -1);
		Debug.Log ("Target : ("+randomX+","+randomZ+")");

		for(int x = (int)levelSize.x -1  ; x > -1 ; x--){
			for(int z =  (int)levelSize.z -1 ; z > -1 ; z--){

				Vector3 levelPosition = CoordToPosition (x, z);
				Transform tempCube = Instantiate (cubePrefab, levelPosition, Quaternion.identity)as Transform;
				//tempCube.localScale = Vector3.one  * (1 - outline);
			
				float xScale = 1f * (1 - outline);
				float zScale = 1f * (1 - outline);
				float yScale = tempCube.localScale.y;
				string tempName = "("+x+","+z+")";
				tempCube.name = tempName;

				if(tempCube.position.x == randomX && tempCube.position.z == randomZ){

					tempCube.gameObject.GetComponent<Renderer> ().material = targetMaterial;
					tempCube.gameObject.GetComponent<Destroyable> ().enabled = false;
					tempCube.name = "Target Cube";
				}

				if(tempCube.position.x == -1f && tempCube.position.z == -1f){

					tempCube.gameObject.GetComponent<Renderer> ().material = targetMaterial;
					tempCube.gameObject.GetComponent<Destroyable> ().enabled = false;
					tempCube.name = "Starter Cube";
					
				}



				tempCube.localScale = new Vector3 (xScale, yScale , zScale);

				tempCube.parent = levelHolder;
			}
		}
	}
}
