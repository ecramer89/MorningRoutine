using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CharacterAggregate : Aggregate {
	StoryNode rootNode; //root node of the current story tree (call it story tree instead of story line).
	StoryNode currentNode; //a reference to the specific node in the dialogue tree that the player is currently on.
	HashSet<string> completedStoryIds; //will probably just dequeue them.

	public override Event[] execute(Command command){

		if (command.GetType () == typeof(CreateCharacter)) {
			return this.CreateCharacter ((CreateCharacter)command);
		}
		if (command.GetType () == typeof(AddNodeToStory)) {
			return this.AddNodeToStory ((AddNodeToStory)command);
		}
		if (command.GetType () == typeof(InitiateStory)) {
			return this.InitiateStory ((InitiateStory)command);
		}
		if (command.GetType () == typeof(AdvanceStory)) {
			return this.AdvanceStory ((AdvanceStory)command);
		}
			
		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(CharacterCreated)){
			this.OnCharacterCreated((CharacterCreated)evt);
		}
		if (evt.GetType () == typeof(NodeAddedToStory)) {
			this.OnNodeAddedToStory ((NodeAddedToStory)evt);
		}
		if (evt.GetType () == typeof(StoryInitiated)) {
			this.OnStoryInitiated ((StoryInitiated)evt);
		}
		if (evt.GetType () == typeof(StoryAdvanced)) {
			this.OnStoryAdvanced ((StoryAdvanced)evt);
		}
		if (evt.GetType () == typeof(StoryCompleted)) {
			this.OnStoryCompleted ((StoryCompleted)evt);
		}
	}


	private Event[] CreateCharacter(CreateCharacter command){
		if (this.id != Aggregate.NullId) {
			throw new ValidationException ("id", "Already exists.");
		}
	
		return new Event[] {
			new CharacterCreated(command.name, command.greeting)
		};

	}
	//todo rename to add story node
	private Event[] AddNodeToStory(AddNodeToStory command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}
		if (command.playerResponses.Length != command.characterResponses.Length) {
			throw new ValidationException ("responses", "Each possible player response must match to one charaterResponse. Did you forget to define one in the text file?");
		}
		if (command.introductoryText.Length == 0) {
			throw new ValidationException ("introductoryText", "introductory text can't be empty.");
		}
		if (!ReferenceEquals (this.rootNode, null) && ReferenceEquals (rootNode.Find (command.introductoryText), null)) {
			throw new ValidationException ("introductoryText", "introductory text is neither the root nor an existing line in this story.");
		}
	
		for(int i=0;i<command.playerResponses.Length; i++){
			string playerResponse = command.playerResponses [i];
			string characterResponse = command.characterResponses [i];
			if (playerResponse.Length == 0 || characterResponse.Length == 0) {
				throw new ValidationException ("response", $"Response can't be empty; check the values for response {i+1}; introductory text '{command.introductoryText}'");
			}
		}

	
		string[] playerResponses = ConvertPlayerResponsesToRegexes (command.playerResponses);

	
		return new Event[] {
			new NodeAddedToStory(command.characterName, command.storylineId, 
				command.introductoryText,playerResponses, command.characterResponses, command.eventsToPublishOnReaching)//stub for now
		};
	}

	private Event[] InitiateStory(InitiateStory command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}


		return new Event[] {
			new StoryInitiated(command.characterName, command.playerId, this.rootNode)
		};

	}

	private Event[] AdvanceStory(AdvanceStory command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}
		if (this.currentNode == null) {
			throw new ValidationException ("", "No dialogue initiated.");
		}
		string formattedInput = FormatUserInput (command.input);
		if (formattedInput.Length == 0) {
			throw new ValidationException ("input", "Input can't be empty.");
		}
		if (this.currentNode.IsLeaf ()) {
			//todo it makes sense for storylines to have id, not sure about story nodes within stryline (unless each node has the id of the story) stories should be queued.
			StoryCompleted storylineCompleted = new StoryCompleted (command.characterName, "TEMP-REPLACE WITH STORYLINE ID", command.playerId);
			if (!this.completedStoryIds.Contains (storylineCompleted.storylineId)) {
				return new Event[]{
					storylineCompleted,
					new StoryPrizeAwarded(command.playerId, command.characterName,  currentNode.prizeId)
				};
			}
			return new Event[]{ 
				storylineCompleted
			};
		}
		List<StoryNode> children = this.currentNode.GetChildren (formattedInput);
		if (children == null) {
			throw new ValidationException ("input", $"No response for {command.input}");
		}
		int random = RandomNumberGenerator.Instance.Range(0, children.Count);
		StoryNode newNode = children [random];

		Event[] result = new Event[1 + currentNode.eventsToPublishOnReaching.Length];
		result [0] = new StoryAdvanced (
			command.characterName, command.playerId, 
			command.input, newNode
		);
		for (int i = 1; i < result.Length; i++) {
			result [i] = currentNode.eventsToPublishOnReaching [i - 1];
		}

		return result;
	}


	private void OnCharacterCreated(CharacterCreated evt){
		this.id = evt.name;
		this.currentNode = null;
		this.completedStoryIds = new HashSet<string> ();
	}
		
	private void OnNodeAddedToStory(NodeAddedToStory evt){
		
		StoryNode introductoryTextNode; 
		if (!ReferenceEquals (rootNode, null)) { 
			introductoryTextNode = rootNode.Find (evt.introductoryText);
		} else {
			rootNode=new StoryNode(evt.introductoryText);
			introductoryTextNode = rootNode;
		}
			
		for (int i=0;i<evt.playerResponses.Length;i++) {
			string allowedPlayerResponse = evt.playerResponses [i];
			string characterResponse = evt.characterResponses [i];
			string[] eventsToPublishOnReaching = null;
			StoryNode node = rootNode.Find (characterResponse);
			if(node == null) node = new StoryNode(characterResponse);
			//add child should then accept the entry pattern as one argument and the node as the other.
			introductoryTextNode.AddChild (allowedPlayerResponse,node);
		}

		//events are always in reference to the text for each node, not to the responses.
		//(why there is only ever one of them)
		//consequently, to set an event to be published in response to something a character says,
		//need to specify it in the line of the story csv where that response is the introudctry text.
		introductoryTextNode.eventsToPublishOnReaching = evt.eventsToPublishOnReaching;
	}

	private void OnStoryCompleted(StoryCompleted evt){
		completedStoryIds.Add (evt.storylineId);
	}
		
	private void OnStoryInitiated(StoryInitiated evt){
		this.currentNode = rootNode;
	}

	private void OnStoryAdvanced(StoryAdvanced evt){
		this.currentNode = evt.newNode;
	}

	private string FormatUserInput(string userInput){
		return userInput.Trim ().ToLower ().Replace (" ", "");
	}
		

	private string[] ConvertPlayerResponsesToRegexes(string[] playerResponses){
		return playerResponses.Select (response => @"\w*"+$"{response}"+@"\w*").ToArray();
	}


}
