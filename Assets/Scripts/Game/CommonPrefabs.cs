using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPrefabs : MonoBehaviour {

	public static CommonPrefabs Instance{
		get {
			return instance;
		}
	}
	static CommonPrefabs instance;

	public GameObject Timer;

	void Start(){
		instance = gameObject.GetComponent<CommonPrefabs> ();
	}

}
