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


	CharacterState characterState = new CharacterState (new List<string>());
	public delegate void CharacterStateHandler(string characterName);
	public event CharacterStateHandler characterAdded = (characterName)=>{};
	public event CharacterStateHandler characterSet = (characterName)=>{};
	public CharacterState CharacterState{
		get {
			return characterState;
		}
	}

	public void AddCharacter(string characterName){
		CharacterState.characters.Add (characterName);
		characterAdded (characterName);
	}

	public void SetCharacter(string characterName){
		CharacterState.currentCharacterName = characterName;
		characterSet (characterName);
	}
}


//state classes.
public class PlayerState{
	public string id; 
	public string name;

}

public class MessageState{
	public string message;
}
	
public class CharacterState{
	public List<string> characters;
	public string currentCharacterName;

	public CharacterState(List<string> characters){
		this.characters = characters;
		this.currentCharacterName = Aggregate.NullId;
	}
}
