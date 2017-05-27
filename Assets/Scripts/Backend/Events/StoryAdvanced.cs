

public class StoryAdvanced : Event {
		public string characterName;
		public string playerId;
	    public string input;
	    public StoryNode newNode;

	public StoryAdvanced(string characterName, string playerId, string input, StoryNode newNode){
			this.characterName = characterName;
			this.playerId = playerId;
			this.input = input;
		    this.newNode = newNode;
		}

}
