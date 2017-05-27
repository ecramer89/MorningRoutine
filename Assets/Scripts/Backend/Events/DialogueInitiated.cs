public class DialogueInitiated : Event {
	public string characterName;
	public string playerId;
	public StoryNode initialNode;

	public DialogueInitiated(string characterName, string playerId, StoryNode initialNode){
		this.characterName = characterName;
		this.playerId = playerId;
		this.initialNode = initialNode;
	}
}

