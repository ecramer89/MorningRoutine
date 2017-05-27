

public class AdvanceStory : Command {
	public string characterName;
	public string playerId;
	public string input;

	public AdvanceStory(string characterName, string playerId, string input){
		this.characterName = characterName;
		this.playerId = playerId;
		this.input = input;
	}

}
