using System;

public class CreateCharacter : Command {
	public string name;
	public string greeting;

	public CreateCharacter(string name, string greeting){
		this.name = name;
		this.greeting = greeting;
	}

}