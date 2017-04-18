

public class StorylineCompleted : Event {
	public int characterId;
	public int storylineId;
	public int playerId;
	public StorylineCompleted(int characterId, int storylineId, int playerId){
		this.characterId = characterId;
		this.storylineId = storylineId;
		this.playerId = playerId;
	}

}
