
public class PlayerController  {



	public static ServerResponse BeginGame(){
		
		int playerId = IntegerGUIDCreator.CreateGUID ();
		//stub in for now; eventually have a text field where player can enter name
		string playerName = "Chase";
		Command command = new BeginNewGame (playerId, playerName);
		Aggregate player = new PlayerAggregate ();
		bool success = CommandHandler.HandleCommand(player, playerId, command);
		ServerResponse response = new ServerResponse (playerId, ModelNameGetter.GetModelName(player.GetType()), !success);
		return response;
	}

}
