

public class StorylineCompleted : Event {
	public string characterName;
	public string storylineId;
	public string playerId;
	public StorylineCompleted(string characterName, string storylineId, string playerId){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.playerId = playerId;
	}

}
