
public class StorylinePrizeAwarded : Event {
	public string playerId;
	public string characterName;
	public int prizeId;

	public StorylinePrizeAwarded(string playerId, string characterName, int prizeId){
		this.playerId = playerId;
		this.characterName = characterName;
		this.prizeId = prizeId;

	}

}
