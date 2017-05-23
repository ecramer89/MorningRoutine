

public class AdvanceDialogue : Command {
	public string characterName;
	public string playerId;
	public string input;

	public AdvanceDialogue(string characterName, string playerId, string input){
		this.characterName = characterName;
		this.playerId = playerId;
		this.input = input;
	}

}
