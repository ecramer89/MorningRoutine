
public class PlayerOffendedCharacter : Event {
	public string characterName;
	public string reason;
	public PlayerOffendedCharacter(string characterName,string reason=""){
		this.characterName = characterName;
		this.reason = reason;
	}
}
