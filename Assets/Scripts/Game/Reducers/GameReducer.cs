using System;
using UnityEngine;
public class GameReducer  {


	public static void Reduce(Actions type, ReadModel eventData){
		Debug.Log (type);
		switch (type) {
		case Actions.NEW_GAME_BEGUN:
			GameState state = GameState.Instance;
			state.PlayerId = eventData.Id;
			Debug.Log ("called");
			break;
		}

	}

}
