using System;

public class NewGameBegun : Event {
	public string playerId;
	public string playerName;

	public NewGameBegun(string playerId, string playerName){
		this.playerId = playerId;
		this.playerName = playerName;
	}

}
