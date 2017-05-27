public class StoryInitiated : Event {
	public string characterName;
	public string playerId;
	public StoryNode initialNode;

	public StoryInitiated(string characterName, string playerId, StoryNode initialNode){
		this.characterName = characterName;
		this.playerId = playerId;
		this.initialNode = initialNode;
	}
}

