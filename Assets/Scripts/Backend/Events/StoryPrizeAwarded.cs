
public class StoryPrizeAwarded : Event {
	public string playerId;
	public string characterName;
	public int prizeId;

	public StoryPrizeAwarded(string playerId, string characterName, int prizeId){
		this.playerId = playerId;
		this.characterName = characterName;
		this.prizeId = prizeId;

	}

}
