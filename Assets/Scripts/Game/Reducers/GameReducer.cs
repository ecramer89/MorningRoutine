using System;
using UnityEngine;
public class GameReducer  {

	/*
	 * Always follow the pattern:
	 * -retrieve the current game state object you want to update
	 * -mutate properties on the game state object, using data from the read model
	 * -re-assign the new game state object to the GameState
	 * */
	public static void Reduce(ActionTypes type, System.Object eventData = null){
		GameState state = GameState.Instance;

		switch (type) {
		case ActionTypes.NEW_GAME_BEGUN:
			PlayerReadModel model = (PlayerReadModel)eventData;
			PlayerState currentPlayerState = state.PlayerState;
			currentPlayerState.id = model.Id;
			currentPlayerState.name = model.name;
			GameState.Instance.PlayerState = currentPlayerState;
			break;

		case ActionTypes.MESSAGE_SET:
			string message = (string)eventData;
			MessageState currentMessageState = state.MessageState;
			currentMessageState.message = message;
			state.MessageState = currentMessageState;
			break;

		case ActionTypes.NARRATION_BEGUN:
			NarrationState currentNarrationState = state.NarrationState;
			currentNarrationState.narrationOn = true;
			state.NarrationState = currentNarrationState;
			break;

		case ActionTypes.NARRATION_ENDED:
			currentNarrationState = state.NarrationState;
			currentNarrationState.narrationOn = false;
			state.NarrationState = currentNarrationState;
			break;
		}
	}

}
