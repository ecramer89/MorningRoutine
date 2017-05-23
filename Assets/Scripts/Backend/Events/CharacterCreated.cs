using System;

public class CharacterCreated : Event {
	public string name;
	public string greeting;

	public CharacterCreated(string name, string greeting){
		this.name = name;
		this.greeting = greeting;
	}

}
