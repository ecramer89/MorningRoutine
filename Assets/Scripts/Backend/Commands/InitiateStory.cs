

public class InitiateStory : Command {
	public string characterName;
	public string playerId;

	public InitiateStory(string characterName, string playerId){
		this.characterName = characterName;
		this.playerId = playerId;
	}
}
