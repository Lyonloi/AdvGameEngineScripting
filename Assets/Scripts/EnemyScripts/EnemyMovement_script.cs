﻿using UnityEngine;
using System.Collections;

public class EnemyMovement_script : MonoBehaviour {
	//Vars for refencing this to GameManager
	GameManager_script GMs;
	GameObject GM;

	//vars for getting current position
	Transform CurrentPosition;
	float tempX;
	float tempY;
	float tempZ;

	//var for controlling enemy's speed 
	public float movingSpeed = 0.05f;

	//vars for changing status of the enemy
	public bool isMoving = true;
	public bool isAttacking = false;

	//var for targeting a gameobject to destory
	public GameObject attackTarget;

	// Use this for initialization
	void Start () {

		//refence this GM to GameManager
		GM = GameObject.FindGameObjectWithTag ("GM");
		GMs = GM.GetComponent<GameManager_script>();

		//get the current position
		CurrentPosition = gameObject.transform;
		//store up the vars
		tempX = CurrentPosition.position.x;
		tempY = CurrentPosition.position.y;
		tempZ = CurrentPosition.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		//keep this unit moving if isMoving is true
		if (isMoving == true) {
			Move();	
		}
	}

	void Move(){
		//make this unit moves along with X
		tempX = tempX - movingSpeed;
		transform.position = new Vector3(tempX,tempY, tempZ);
	}

	void MoveAgain(){
		//make this unit moves again
		isMoving = true;
	}

	void DestoryObstacle(){
		//destroy an obstacle if there is one
		if (attackTarget != null) {
						Destroy (attackTarget);
				}
	}

	void OnTriggerEnter(Collider C){

		//if this unit touches any of these trigger 
		if (isMoving == true && C.tag == "wall"||C.tag == "enemy"||C.tag == "rock") {
			//stop moving
			isMoving = false;
			gameObject.rigidbody.useGravity = true;
			//if Collider C is a rock
			if (C.tag == "rock"){
				//make Collider C as the attackTarget and destory it after a few second
				attackTarget = C.gameObject;
				Invoke("DestoryObstacle",2.75f);
				//start moving again
				Invoke("MoveAgain",3f);

			}


		}
		//if Collider C is a bullet, get destoried 
		if (C.tag == "bullet"){

			Destroy(gameObject);
			//add Score
			GMs.AddScore();
		}
		//if Collider C is an explosion, get destoried
		if (C.tag == "explosion"){
			
			Destroy(gameObject);
			//add Score
			GMs.AddScore();
		}

		if (C.tag == "oilOnGround") {
			movingSpeed = 0.01f;
		}
	}

	void OnTriggerExit(Collider C){
		if (C.tag == "oilOnGround") {
			movingSpeed = 0.02f;
		}
	}
}
