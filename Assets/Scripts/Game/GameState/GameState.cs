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
	public event CharacterStateHandler characterSet = (characterId)=>{};
	public CharacterState CharacterState{
		get {
			return characterState;
		}
	}

	public void AddCharacter(int characterId){
		CharacterState.characters.Add (characterId);
		characterAdded (characterId);
	}

	public void SetCharacter(int characterId){
		CharacterState.currentCharacter = characterId;
		characterSet (characterId);
	}
}


//state classes.
public class PlayerState{
	public int id; 
	public string name;

}

public class MessageState{
	public string message;
}
	
public class CharacterState{
	public List<int> characters;
	public int currentCharacter;
	public CharacterState(List<int> characters){
		this.characters = characters;
		this.currentCharacter = Aggregate.NullId;
	}
}
