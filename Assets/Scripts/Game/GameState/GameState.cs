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


	public delegate void PlayerIdHandler(int playerId);
	public event PlayerIdHandler playerIdSet;
	int playerId;
	public int PlayerId{
		get {
			return playerId;
		}
		set {
			playerId = value;
			playerIdSet (playerId);
		}

	}


	public delegate void MessageHandler(string newMessage, string oldMessage);
	public event MessageHandler messageSet;
	string message;
	public string Message{
		get{
			return message;
		}
		set {
			string oldMessage = message;
			message = value;
			messageSet (message, oldMessage); 
		}
	}
}
