using System;

public class NPCReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(NPCCreated)){
			NPCCreated npcCreated = (NPCCreated)evt;
			table.InsertModel (npcCreated.npcID, new NPCReadModel(npcCreated.npcID, npcCreated.name, npcCreated.greeting));
			return;
		}
	}

}


