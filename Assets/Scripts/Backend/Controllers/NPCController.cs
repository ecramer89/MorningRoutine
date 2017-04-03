using System;

public class NPCController {
	
	public static ServerResponse CreateNPC(int npcId, string npcName, string npcGreeting){
		Command command = new CreateNPC (npcId, npcName, npcGreeting);
		Aggregate npc = new NPCAggregate ();
		bool success = CommandHandler.HandleCommand(npc, npcId, command);
		ServerResponse response = new ServerResponse (npcId, ModelNameGetter.GetModelName(npc.GetType()), !success);
		return response;
	}
}



