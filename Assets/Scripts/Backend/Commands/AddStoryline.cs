

public class AddStoryline : Command {
	public int characterId;
	public int storylineId;
	public string parent;
	public string entryPattern; //must have syntax of regular expression pattern. e.g. @"\d+"
	public string[] steps;
	public float requiredLevel;
	public int[] completeFirst;
	public string text;

	public AddStoryline(int characterId, int storylineId, string parent, 
		string entryPattern, string[] steps, string text, float requiredLevel = 0, 
		int[] completeFirst= null){
		this.characterId = characterId;
		this.storylineId = storylineId;
		this.parent = parent;
		this.entryPattern = entryPattern;
		this.steps = steps;
		this.requiredLevel = requiredLevel;
		this.completeFirst = completeFirst;
		this.text = text;
	}

}
