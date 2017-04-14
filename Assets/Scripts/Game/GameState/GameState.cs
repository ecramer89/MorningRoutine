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


	CharacterState characterState = new CharacterState (new List<int>());
	public delegate void CharacterStateHandler(int id);
	public event CharacterStateHandler characterAdded = (characterId)=>{};
	public CharacterState CharacterState{
		get {
			return characterState;
		}
	}

	public void AddCharacter(int id){
		CharacterState.characters.Add (id);
		characterAdded (id);
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
	
public struct CharacterState{
	public List<int> characters;
	public CharacterState(List<int> characters){
		this.characters = characters;
	}
}
