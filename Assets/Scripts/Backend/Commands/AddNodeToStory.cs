

public class AddNodeToStory : Command {
	public string characterName;
	public string storylineId;
	public string introductoryText;
	public string[] playerResponses; //must have syntax of regular expression pattern. e.g. @"\d+"
	public string[] characterResponses;
	public Event[] eventsToPublishOnReaching;

	public AddNodeToStory(string characterName, string storylineId, string introductoryText, 
		string[] playerResponses, string[] characterResponses, Event[] eventsToPublishOnReaching = null){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.introductoryText = introductoryText;
		this.playerResponses = playerResponses;
		this.characterResponses = characterResponses;
		this.eventsToPublishOnReaching = eventsToPublishOnReaching;
	}

}
