using System;

public class NPCCreated : Event {
	public int npcID;
	public string name;
	public string greeting;

	public NPCCreated(int id, string name, string greeting){
		this.npcID = id;
		this.name = name;
		this.greeting = greeting;
	}

}
