using System;
public class BeginNewGame : Command {

	public int playerId;
	public string playerName;

	public BeginNewGame(int playerId, string playerName){
		this.playerId = playerId;
		this.playerName = playerName;
	}
}
