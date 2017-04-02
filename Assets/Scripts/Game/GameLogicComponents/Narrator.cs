using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour {


	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		GameState.Instance.playerIdSet += (int playerId) => {
			gameObject.SetActive (true);
			ActionCreator.Instance.SetMessage("Hello");
		};
		
	}

}
