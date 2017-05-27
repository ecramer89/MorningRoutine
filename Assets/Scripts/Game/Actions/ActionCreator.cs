using UnityEngine;

public enum ActionTypes {
	NEW_GAME_BEGUN, 
	MESSAGE_SET, 
	CHARACTER_CREATED,
	CHARACTER_SET,
	CURRENT_STORYLINE_COMPLETED
};

public class ActionCreator : MonoBehaviour  {
	
	static ActionCreator instance;
	public static ActionCreator Instance{
		get{
			return instance;
		}
	}

	void Awake(){
		instance = gameObject.GetComponent<ActionCreator> ();
	}

	public void BeginNewGame(){
		ServerResponse response = PlayerController.BeginGame ();
		if (!response.error) {
			ReadModel player = ModelRepository.Get (response.modelName,response.aggregateIdentifier);
			GameReducer.Reduce (ActionTypes.NEW_GAME_BEGUN, player);
			Debug.Log ($"Success: New Player");
		} else
			Debug.Log ("Failed");
	}

	public void CreateCharacter(string characterName,string greeting){
		ServerResponse response = CharacterController.CreateCharacter (characterName, greeting);
		if (!response.error) {
			ReadModel character = ModelRepository.Get (response.modelName,response.aggregateIdentifier);
			GameReducer.Reduce (ActionTypes.CHARACTER_CREATED, character);
			Debug.Log ($"Success: New Character {name}");
		} else
			Debug.Log ("Failed");
	}

	public void AddNodeToStory(string characterName, string storyLineId, string introductoryText, string[] playerResponses, string[] characterResponses, Event[] eventsToPublishOnReaching = null){
		ServerResponse response = CharacterController.AddNodeToStory (characterName, storyLineId, introductoryText, playerResponses,characterResponses, eventsToPublishOnReaching);
		if (!response.error) {
			Debug.Log ($"Success: New StoryLine");
		} else
			Debug.Log ("Failed");
	}
	//TODO be explicit- dispatch actions like DIALOGUE_INITIATED, dialogue_advanced and have the gae reducer translate these into 
	//more specific ui changes such as "message set" and "character se". gets confusing otherwsie.
	public void InitiateStory(string characterName){
		string playerId = GameState.Instance.PlayerState.id;
		ServerResponse response = CharacterController.InitiateStory (characterName, playerId);
		if (!response.error) {
			Debug.Log ($"Success: Initiating dialogue");
			CharacterReadModel character = (CharacterReadModel)ModelRepository.Get (response.modelName,response.aggregateIdentifier);
			GameReducer.Reduce (ActionTypes.MESSAGE_SET, character.currentText);
			GameReducer.Reduce (ActionTypes.CHARACTER_SET, character);
		} else
			Debug.Log ("Failed");
	}

	public void AdvanceStory(string userInput){
		string characterName = GameState.Instance.CharacterState.currentCharacterName;
		string playerId = GameState.Instance.PlayerState.id;
		ServerResponse response = CharacterController.AdvanceStory (characterName, playerId, userInput);
		if (!response.error) {
			Debug.Log ($"Success: Advanced dialogue");
			CharacterReadModel character = (CharacterReadModel)ModelRepository.Get (response.modelName,response.aggregateIdentifier);
			GameReducer.Reduce (ActionTypes.MESSAGE_SET, character.currentText);
			if (character.currentStorylineCompleted == true) {
				GameReducer.Reduce (ActionTypes.CURRENT_STORYLINE_COMPLETED, "");
			}
		} else
			Debug.Log ("Failed");
	}

	public void SetMessage(string message){
		GameReducer.Reduce (ActionTypes.MESSAGE_SET, message);
	}
					

}
