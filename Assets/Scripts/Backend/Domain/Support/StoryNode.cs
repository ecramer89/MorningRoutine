using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StoryNode  {
	public string id;
	public string text; //the text that is displayed for this story node.
	public float requiredLevel; //the required degree of familiarity (floating point between 0 and 1) the player needs to have with this character to be ableto explore this story)
	//(node, given player read moel adictionary of character id to degree of familiarity)
	public string allowedPlayerResponse; //the input text that leads to this node from its parent
	public Dictionary<string, List<StoryNode>> children; //the storynodes you can reach from this storynode.
	public int prizeId;
	 
	public StoryNode(string id, string text, string allowedPlayerResponse=""){
		this.id = id;
		this.text = text;
		this.allowedPlayerResponse = allowedPlayerResponse;
		this.children = new Dictionary<string, List<StoryNode>> ();
	}

	public void AddChild(string playerResponseLeadingToNode, StoryNode child){
		string entryPattern = playerResponseLeadingToNode;
		List<StoryNode> others;
		if (children.TryGetValue (entryPattern, out others)) {
			others.Add (child);
		} else {
			children.Add (entryPattern, new List<StoryNode>{child});
		}

	}

	//BFS; find the shallowest node whose text matches that of the specified parent text.
	public StoryNode FindParent(string text){
		if(Regex.Matches(this.text, text, RegexOptions.IgnoreCase).Count > 0) return this;
		Queue<StoryNode> toVisit = new Queue<StoryNode> ();
		foreach (StoryNode child in GetChildren()) {
			toVisit.Enqueue (child);
		}
		while (toVisit.Count > 0) {
			StoryNode next = toVisit.Dequeue ();
			if (Regex.Matches(next.text, text, RegexOptions.IgnoreCase).Count > 0) {
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
			if (Regex.Matches (userInput,regex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace).Count > 0)
				return children [regex];
		}

		return null;
	}

	public bool IsRoot(){
		return children.Count == 0;
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
		
	public static StoryNode ToStoryNode(string id, string introductoryText, string allowedPlayerResponse){
		return new StoryNode(id, introductoryText, allowedPlayerResponse);
	}
		


}


