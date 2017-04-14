

public class DialogueAdvanced : Event {
		public int characterId;
		public int playerId;
	    public string input;
	    public StoryNode newNode;

	public DialogueAdvanced(int characterId, int playerId, string input, StoryNode newNode){
			this.characterId = characterId;
			this.playerId = playerId;
			this.input = input;
		    this.newNode = newNode;
		}

}
