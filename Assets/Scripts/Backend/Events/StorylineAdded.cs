
public class AddStorylineAdded : Event {
	public int characterId;
	public int storylineId;
	public string parent;
	public string entryPattern;
	public StoryNodeData[] steps;
	public float requiredLevel;
	public int[] completeFirst;
	public string text;

	public AddStorylineAdded(int characterId, int storylineId, string parent, 
		string entry, StoryNodeData[] steps, string text, float requiredLevel = 0, 
		int[] completeFirst= null){
		this.characterId = characterId;
		this.storylineId = storylineId;
		this.parent = parent;
		this.text = text;
		this.entryPattern = entry;
		this.steps = steps;
		this.requiredLevel = requiredLevel;
		this.completeFirst = completeFirst;
	}

}

