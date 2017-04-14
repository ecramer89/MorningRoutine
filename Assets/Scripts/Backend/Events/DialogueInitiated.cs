public class DialogueInitiated : Event {
	public int characterId;
	public int playerId;

	public DialogueInitiated(int characterId, int playerId){
		this.characterId = characterId;
		this.playerId = playerId;
	}
}

