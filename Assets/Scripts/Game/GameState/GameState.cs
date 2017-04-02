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


	PlayerState playerState = new PlayerState();
	public delegate void PlayerStateHandler();
	public event PlayerStateHandler playerFetched=()=>{};
	public PlayerState PlayerState{
		get {
			return playerState;
		}
		set {
			playerState = value;
			playerFetched();
		}
	}


	MessageState messageState = new MessageState();
	public delegate void MessageStateHandler();
	public event MessageStateHandler messageSet=()=>{};

	public MessageState MessageState{
		get{
			return messageState;
		}
		set {
			messageState = value;
			messageSet (); 
		}
	}

	NarrationState narrationState = new NarrationState ();
	public delegate void NarrationStateHandler();
	public event NarrationStateHandler narrationSet=()=>{};

	public NarrationState NarrationState{
		get{
			return narrationState;
		}
		set{
			narrationState = value;
			narrationSet ();
		}
	}
}


//state structs.
public struct PlayerState{
	public int id; 
	public string name;

}

public struct MessageState{
	public string message;
}
	
public struct NarrationState{
	public bool narrationOn;
}
