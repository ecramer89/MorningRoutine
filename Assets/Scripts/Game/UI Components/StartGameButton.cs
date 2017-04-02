using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {

	void Start () {
		GameState.Instance.playerFetched += () => {
			gameObject.SetActive (false);
		};
	}
}
