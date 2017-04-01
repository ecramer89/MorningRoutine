using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerInterface : MonoBehaviour {
	//"action creators"
	public void BeginNewGame(){
		ServerResponse response = PlayerController.BeginGame ();
		if (!response.error) {
			ReadModel player = ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (Actions.NEW_GAME_BEGUN, player);
			Debug.Log ($"Success: New Player {GameState.Instance.PlayerId}");
		} else
			Debug.Log ("Failed");
	}
}
