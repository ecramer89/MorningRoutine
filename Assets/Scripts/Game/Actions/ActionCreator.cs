using UnityEngine;

public enum ActionTypes {
	NEW_GAME_BEGUN, 
	MESSAGE_SET, 
	CHARACTER_CREATED,
	CHARACTER_SET
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
			ReadModel player = ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.NEW_GAME_BEGUN, player);
			Debug.Log ($"Success: New Player");
		} else
			Debug.Log ("Failed");
	}

	public void CreateCharacter(int npcId, string npcName, string npcGreeting){
		ServerResponse response = CharacterController.CreateCharacter (npcId, name, npcGreeting);
		if (!response.error) {
			ReadModel character = ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.CHARACTER_CREATED, character);
			Debug.Log ($"Success: New Character {name}");
		} else
			Debug.Log ("Failed");
	}

	public void AddStoryLine(int characterId, int storyLineId, string parentText, string entryPattern, string text, StoryNodeData[] steps){
		ServerResponse response = CharacterController.AddStoryLine (characterId, storyLineId, parentText, entryPattern, 
			text, steps);
		if (!response.error) {
			Debug.Log ($"Success: New StoryLine");
		} else
			Debug.Log ("Failed");
	}

	public void InitiateDialogue(int characterId){
		int playerId = GameState.Instance.PlayerState.id;
		ServerResponse response = CharacterController.InitiateDialogue (characterId, playerId);
		if (!response.error) {
			Debug.Log ($"Success: Initiating dialogue");
			CharacterReadModel character = (CharacterReadModel)ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.MESSAGE_SET, character.greeting);
			GameReducer.Reduce (ActionTypes.CHARACTER_SET, character);
		} else
			Debug.Log ("Failed");
	}

	public void AdvanceDialogue(string userInput){
		int characterId = GameState.Instance.CharacterState.currentCharacter;
		int playerId = GameState.Instance.PlayerState.id;
		ServerResponse response = CharacterController.AdvanceDialogue (characterId, playerId, userInput);
		if (!response.error) {
			Debug.Log ($"Success: Advanced dialogue");
			CharacterReadModel character = (CharacterReadModel)ModelRepository.Get (response.modelName,response.aggregateId);
			GameReducer.Reduce (ActionTypes.MESSAGE_SET, character.currentText);
		} else
			Debug.Log ("Failed");
	}

	public void SetMessage(string message){
		GameReducer.Reduce (ActionTypes.MESSAGE_SET, message);
	}
					

}
