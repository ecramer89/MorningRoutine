using System;

public class NewGameBegun : Event {
	public int playerId;
	public string playerName;

	public NewGameBegun(int playerId, string playerName){
		this.playerId = playerId;
		this.playerName = playerName;
	}

}
