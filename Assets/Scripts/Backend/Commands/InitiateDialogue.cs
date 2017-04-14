

public class InitiateDialogue : Command {
	public int characterId;
	public int playerId;

	public InitiateDialogue(int characterId, int playerId){
		this.characterId = characterId;
		this.playerId = playerId;
	}
}
