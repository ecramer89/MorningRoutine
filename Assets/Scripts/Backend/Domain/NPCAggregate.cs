using System;

public class NPCAggregate : Aggregate {

	public override Event[] execute(Command command){

		if (command.GetType () == typeof(CreateNPC)) {
			return this.CreateNPC ((CreateNPC)command);
		}
			
		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(NPCCreated)){
			this.NPCCreated((NPCCreated)evt);
		}

	}


	private Event[] CreateNPC(CreateNPC command){
		if (this.id != Aggregate.NullId) {
			throw new ValidationException ("id", "Already exists.");
		}
	
		return new Event[] {
			new NPCCreated(command.npcID, command.name, command.greeting)
		};

	}



	private void NPCCreated(NPCCreated evt){
		this.id = evt.npcID;
	}
}
