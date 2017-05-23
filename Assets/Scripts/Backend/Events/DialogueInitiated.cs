public class DialogueInitiated : Event {
	public string characterName;
	public string playerId;

	public DialogueInitiated(string characterName, string playerId){
		this.characterName = characterName;
		this.playerId = playerId;
	}
}

