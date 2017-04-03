using UnityEngine;

public enum ActionTypes {NEW_GAME_BEGUN, MESSAGE_SET, NARRATION_BEGUN, NARRATION_ENDED, NPC_CREATED};

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

	public void CreateNPC(int npcId, string npcName, string npcGreeting){
		serverInterface.CreateNPC (npcId, npcName, npcGreeting);
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
