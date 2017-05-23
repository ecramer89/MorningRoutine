using System;
public class BeginNewGame : Command {

	public string playerId;
	public string playerName;

	public BeginNewGame(string playerId, string playerName){
		this.playerId = playerId;
		this.playerName = playerName;
	}
}
