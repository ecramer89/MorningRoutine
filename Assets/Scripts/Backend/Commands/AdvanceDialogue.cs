

public class AdvanceDialogue : Command {
	public int characterId;
	public int playerId;
	public string input;

	public AdvanceDialogue(int characterId, int playerId, string input){
		this.characterId = characterId;
		this.playerId = playerId;
		this.input = input;
	}

}
