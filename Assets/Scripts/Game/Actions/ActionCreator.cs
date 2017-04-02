using UnityEngine;

public enum ActionTypes {NEW_GAME_BEGUN, MESSAGE_SET, NARRATION_BEGUN, NARRATION_ENDED};

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
