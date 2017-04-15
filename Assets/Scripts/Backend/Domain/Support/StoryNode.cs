using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public struct StoryNodeData {
	public string text;
	public string entryPattern;
	public StoryNodeData(string text, string entryPattern){
		this.text=text;
		this.entryPattern = entryPattern;
	}
}

public class StoryNode  {
	public int id;
	public string parent; //the message of the storynode that would lead to this storynode.
	public float requiredLevel; //the required degree of familiarity (floating point between 0 and 1) the player needs to have with this character to be ableto explore this story)
	//(node, given player read moel adictionary of character id to degree of familiarity)
	public HashSet<int> completeFirst; //ids of storylines that the player needs to have completed (nb put this data in player read model,
	//pass into the "advance story" command) in order to visit this story. defaults to empty.
	public string entryPattern; //the input text that leads to this node from its parent
	public string text; //the message that we display on the screen when we are at this step in the story
	public Dictionary<string, List<StoryNode>> children; //the storynodes you can reach from this storynode.

	 
	public StoryNode(int id, string parent, string entry, string text, float requiredLevel = 0){
		this.id = id;
		this.parent = parent;
		this.entryPattern = entry;
		this.text = text;
		this.requiredLevel = requiredLevel;
		this.completeFirst = new HashSet<int> ();
		this.children = new Dictionary<string, List<StoryNode>> ();
	}

	public void AddChild(StoryNode child){
		string entryPattern = child.entryPattern;
		List<StoryNode> others;
		if (children.TryGetValue (entryPattern, out others)) {
			others.Add (child);
		} else {
			children.Add (entryPattern, new List<StoryNode>{child});
		}

	}

    //BFS
	//switch == to a case insensitive comparison.
	public StoryNode FindParent(string parentText){
		if(Regex.Matches(this.text, parentText, RegexOptions.IgnoreCase).Count > 0) return this;
		Queue<StoryNode> toVisit = new Queue<StoryNode> ();
		foreach (StoryNode child in GetChildren()) {
			toVisit.Enqueue (child);
		}
		while (toVisit.Count > 0) {
			StoryNode next = toVisit.Dequeue ();
			if (Regex.Matches(next.text, parentText, RegexOptions.IgnoreCase).Count > 0) {
				return next;
			}
			foreach (StoryNode grandChild in next.GetChildren()) {
				toVisit.Enqueue (grandChild);
			}

		}

		return null;
	}

	//return the first setof children that are s.t. the user's input matches the entry regex for those children.
	//return null if input doesn't match any regexes.
	public List<StoryNode> GetChildren(string userInput){
		foreach (string regex in children.Keys) {
			if (Regex.Matches (userInput, regex, RegexOptions.IgnoreCase).Count > 0)
				return children [regex];
		}

		return null;
	}

	public List<StoryNode> GetChildren(){
		List<StoryNode> result = new List<StoryNode>();
		foreach (List<StoryNode> childSet in children.Values) {
			foreach (StoryNode child in childSet) {
				result.Add (child);
			}
		}
		return result;
	}

	public void AddCompleteFirst(int storyLineId){
		completeFirst.Add (storyLineId);
	}

	//e.g., length =2 
	public static StoryNode ToStoryNode(int id, string parentText, string entryPattern, string text, StoryNodeData[] steps){
		return ToStoryNode (id, parentText, entryPattern, text, steps, 0);
	}
		
	static StoryNode ToStoryNode(int id, string parentText, string entryPattern, string text, StoryNodeData[] steps, int stepIndex){
		StoryNode root = new StoryNode(id, parentText, entryPattern, text);
		if (stepIndex < steps.Length) {
			root.AddChild (ToStoryNode (id, text, steps [stepIndex].entryPattern, steps [stepIndex].text, steps, stepIndex + 1));
		}
		return root;
	}


}


