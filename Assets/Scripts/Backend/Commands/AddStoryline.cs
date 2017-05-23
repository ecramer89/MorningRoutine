

public class AddStoryline : Command {
	public string characterName;
	public string storylineId;
	public string introductoryText;
	public string[] playerResponses; //must have syntax of regular expression pattern. e.g. @"\d+"
	public string[] characterResponses;

	public AddStoryline(string characterName, string storylineId, string introductoryText, 
		string[] playerResponses, string[] characterResponses){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.introductoryText = introductoryText;
		this.playerResponses = playerResponses;
		this.characterResponses = characterResponses;
	}

}
