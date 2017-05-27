

public class StoryCompleted : Event {
	public string characterName;
	public string storylineId;
	public string playerId;
	public StoryCompleted(string characterName, string storylineId, string playerId){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.playerId = playerId;
	}

}
