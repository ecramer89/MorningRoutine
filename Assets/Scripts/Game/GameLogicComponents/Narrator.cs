using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour {


	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		GameState.Instance.playerFetched += () => {
			gameObject.SetActive (true);
			string playerName = GameState.Instance.PlayerState.name;
			ActionCreator.Instance.SetMessage($"Hello {playerName}.");
		};
		
	}

}
