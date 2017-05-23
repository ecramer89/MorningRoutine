
public class AddStorylineAdded : Event {
	public string characterName;
	public string storylineId;
	public string introductoryText;
	public string[] playerResponses;
	public string[] characterResponses;

	public AddStorylineAdded(string characterName, string storylineId, string introductoryText, 
		string[] playerResponses, string[] characterResponses){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.introductoryText = introductoryText;
		this.playerResponses = playerResponses;
		this.characterResponses = characterResponses;
	
	}

}

