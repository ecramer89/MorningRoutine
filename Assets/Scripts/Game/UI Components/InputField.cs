using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		GameState.Instance.messageSet += () => {
			gameObject.SetActive (true);
		};
	}
	

}
