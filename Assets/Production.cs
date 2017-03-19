using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
		if (Environment.Running != Environment.Type.Production) {
			gameObject.SetActive (false);
		}
	}

}
