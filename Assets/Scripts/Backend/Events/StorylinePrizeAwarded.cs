
public class StorylinePrizeAwarded : Event {
	public int playerId;
	public int characterId;
	public int prizeId;

	public StorylinePrizeAwarded(int playerId, int characterId, int prizeId){
		this.playerId = playerId;
		this.characterId = characterId;
		this.prizeId = prizeId;

	}

}
