using UnityEngine;

public enum ActionTypes {NEW_GAME_BEGUN, MESSAGE_SET, NARRATION_BEGUN, NARRATION_ENDED, CHARACTER_CREATED};

public class ActionCreator : MonoBehaviour  {
	GameServerInterface serverInterface;
	static ActionCreator instance;
	public static ActionCreator Instance{
		get{
			return instance;
		}

	}

	void Awake(){
		serverInterface = new GameServerInterface ();
		instance = gameObject.GetComponent<ActionCreator> ();
	}

	public void BeginNewGame(){
		serverInterface.BeginNewGame ();
	}

	public void CreateCharacter(int npcId, string npcName, string npcGreeting){
		
		serverInterface.CreateCharacter (npcId, npcName, npcGreeting);
	}

	public void AddStoryLine(int characterId, int storyLineId, string parentText, string entryPattern, string text, StoryNodeData[] steps){
		serverInterface.AddStoryLine (characterId, storyLineId, parentText, entryPattern, text, steps);
	}

	public void SetMessage(string message){
		GameReducer.Reduce (ActionTypes.MESSAGE_SET, message);
	}

	public void BeginNarration(){
		GameReducer.Reduce (ActionTypes.NARRATION_BEGUN);
	}

	public void EndNarration(){
		GameReducer.Reduce (ActionTypes.NARRATION_ENDED);
	}

}
