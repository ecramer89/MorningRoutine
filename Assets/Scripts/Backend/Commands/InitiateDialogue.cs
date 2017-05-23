

public class InitiateDialogue : Command {
	public string characterName;
	public string playerId;

	public InitiateDialogue(string characterName, string playerId){
		this.characterName = characterName;
		this.playerId = playerId;
	}
}
