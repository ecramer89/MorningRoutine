using System;
using UnityEngine;
public class GameReducer  {


	public static void Reduce(ActionTypes type, System.Object eventData){
		GameState state = GameState.Instance;
		switch (type) {
		case ActionTypes.NEW_GAME_BEGUN:
			ReadModel model = (ReadModel)eventData;
			state.PlayerId = model.Id;
			break;

		case ActionTypes.MESSAGE_SET:
			string message = (string)eventData;
			state.Message = message;
			break;
		}
	}

}
