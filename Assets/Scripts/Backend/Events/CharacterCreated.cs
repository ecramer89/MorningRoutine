using System;

public class CharacterCreated : Event {
	public int characterId;
	public string name;
	public string greeting;

	public CharacterCreated(int id, string name, string greeting){
		this.characterId = id;
		this.name = name;
		this.greeting = greeting;
	}

}
