
public class NodeAddedToStory : Event {
	public string characterName;
	public string storylineId;
	public string introductoryText;
	public string[] playerResponses;
	public string[] characterResponses;
	public Event[] eventsToPublishOnReaching;

	public NodeAddedToStory(string characterName, string storylineId, string introductoryText, 
		string[] playerResponses, string[] characterResponses, Event[] eventsToPublishOnReaching = null){
		this.characterName = characterName;
		this.storylineId = storylineId;
		this.introductoryText = introductoryText;
		this.playerResponses = playerResponses;
		this.characterResponses = characterResponses;
		this.eventsToPublishOnReaching = eventsToPublishOnReaching;
	
	}

}

