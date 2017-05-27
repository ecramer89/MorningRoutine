public class PlayerLearnedAboutCharacter : Event {

	public string characterName;
	public PlayerLearnedAboutCharacter(string characterName){
		this.characterName = characterName;
	}
}
