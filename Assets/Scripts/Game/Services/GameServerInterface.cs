using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerInterface : MonoBehaviour {
	//"action creators"
	public void CreateDay(){
		ServerResponse response = PlayerController.BeginGame ();
		if (!response.error) {
			GameState state = GameState.Instance;
			state.PlayerId = response.aggregateId;
			Debug.Log ($"Success: New Player {state.PlayerId}");
		} else
			Debug.Log ("Failed");
	}
}
