using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {

	void Start () {
		GameState.Instance.playerIdSet += (int playerId) => {
			gameObject.SetActive (false);
		};
	}
}
