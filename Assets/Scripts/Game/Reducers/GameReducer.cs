using System;
using UnityEngine;
public class GameReducer  {

	/*
	 * Always follow the pattern:
	 * -retrieve the current game state object you want to update
	 * -mutate properties on the game state object, using data from the read model
	 * -re-assign the new game state object to the GameState
	 * 
	 * todo: maybe have a map of delegates to action types so we dont need to worry about the silly duplicate variable names thing
	 * */
	public static void Reduce(ActionTypes type, System.Object eventData = null){
		GameState state = GameState.Instance;

		switch (type) {
		case ActionTypes.NEW_GAME_BEGUN:
			PlayerReadModel playerReadModel = (PlayerReadModel)eventData;
			PlayerState currentPlayerState = state.PlayerState;
			currentPlayerState.id = playerReadModel.Id;
			currentPlayerState.name = playerReadModel.name;
			GameState.Instance.PlayerState = currentPlayerState;
			break;

		case ActionTypes.MESSAGE_SET:
			string message = (string)eventData;
			MessageState currentMessageState = state.MessageState;
			currentMessageState.message = message;
			state.MessageState = currentMessageState;
			break;

		case ActionTypes.CHARACTER_CREATED:
			state.AddCharacter (((CharacterReadModel)eventData).Id);
			break;

		case ActionTypes.CHARACTER_SET:
			state.SetCharacter (((CharacterReadModel)eventData).Id);
			break;

		case ActionTypes.CURRENT_STORYLINE_COMPLETED:
			state.SetCharacter (Constants.NULL_ID);
			break;

		}
	}

}
