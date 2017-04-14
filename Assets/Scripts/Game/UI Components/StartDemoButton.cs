using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDemoButton : MonoBehaviour {

	void Start () {
		gameObject.SetActive (false);
		GameState.Instance.npcAdded += (characterId) => {
			gameObject.SetActive (true);
		};

		GameState.Instance.messageSet += () => {
			gameObject.SetActive (false);
		};
			
	}	
}
