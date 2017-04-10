using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//invoke through the action creator game object
public class GameServerInterface {

	public void BeginNewGame(){
		ServerResponse response = PlayerController.BeginGame ();
		if (!response.error) {
			ReadModel player = ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.NEW_GAME_BEGUN, player);
			Debug.Log ($"Success: New Player");
		} else
			Debug.Log ("Failed");
	}


	public void CreateCharacter(int id, string name, string greeting){
		
		ServerResponse response = CharacterController.CreateCharacter (id, name, greeting);
		if (!response.error) {
			
			ReadModel character = ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.CHARACTER_CREATED, character);
			Debug.Log ($"Success: New Character {name}");
		} else
			Debug.Log ("Failed");

	}

	public void AddStoryLine(int characterId, int storyLineId, string parentText, string entryPattern, 
		string text, StoryNodeData[] steps){
		ServerResponse response = CharacterController.AddStoryLine (characterId, storyLineId, parentText, entryPattern, 
			text, steps);
		if (!response.error) {
			Debug.Log ($"Success: New StoryLine");
		} else
			Debug.Log ("Failed");

	}
}
