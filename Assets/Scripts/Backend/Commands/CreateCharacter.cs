using System;

public class CreateCharacter : Command {
	public int characterId;
	public string name;
	public string greeting;

	public CreateCharacter(int id, string name, string greeting){
		this.characterId = id;
		this.name = name;
		this.greeting = greeting;
	}

}