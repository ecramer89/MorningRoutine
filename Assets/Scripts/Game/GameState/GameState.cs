using System.Collections;
using System.Collections.Generic;


public class GameState {
	//GameState is a Singleton.
	static GameState instance = null;
	public static GameState Instance{
		get {
			if(instance == null) instance = new GameState();
			return instance;
		}
	}




	public PlayerState playerState = new PlayerState();
	public delegate void PlayerIdHandler(int playerId);
	public event PlayerIdHandler playerIdSet;
	int playerId;
	public int PlayerId{
		get {
			return playerId;
		}
		set {
			playerState.id = value;
			playerIdSet (playerState.id);
		}

	}

	public MessageState messageState = new MessageState();
	public delegate void MessageHandler(string newMessage, string oldMessage);
	public event MessageHandler messageSet;

	public string Message{
		get{
			return messageState.message;
		}
		set {
			string oldMessage = messageState.message;
			messageState.message = value;
			messageSet (messageState.message, oldMessage); 
		}
	}
}


//state structs
public struct PlayerState{
	public int id; 
	public string name;

}

public struct MessageState{
	public string message;
}
