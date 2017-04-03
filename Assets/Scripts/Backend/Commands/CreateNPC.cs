using System;

public class CreateNPC : Command {
	public int npcID;
	public string name;
	public string greeting;

	public CreateNPC(int id, string name, string greeting){
		this.npcID = id;
		this.name = name;
		this.greeting = greeting;
	}

}