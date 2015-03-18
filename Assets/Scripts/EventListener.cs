﻿using UnityEngine;
using System.Collections;

public class EventListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		delegate21.onPoweredUp += OnPoweredUp;
	}

	void OnDisable(){
		delegate21.onPoweredUp -= OnPoweredUp;
	}

	void OnPoweredUp(bool isPoweredUp){
		Debug.Log(isPoweredUp);
	}


}
