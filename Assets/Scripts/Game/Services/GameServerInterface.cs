using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerInterface : MonoBehaviour {
	//"action creators"
	public void CreateDay(){
		ServerResponse response = PlayerController.BeginGame ();
		if (!response.error) {
			//would prefer to factor this into the game state itself; have game state or related reducer like functions accept
			//the updated read model (effectively a piece of state) and update themselves).
			ReadModel player = ModelRepository.Get (response.modelName,response.aggregateId);
			GameState state = GameState.Instance;
			state.PlayerId = player.Id;
			Debug.Log ($"Success: New Player {state.PlayerId}");
		} else
			Debug.Log ("Failed");
	}
}
