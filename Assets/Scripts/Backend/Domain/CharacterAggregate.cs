using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterAggregate : Aggregate {

	StoryNode rootNode; //root node of the current story tree (call it story tree instead of story line).
	StoryNode currentNode; //a reference to the specific node in the dialogue tree that the player is currently on.
	HashSet<string> completedStorylineIds;

	public override Event[] execute(Command command){

		if (command.GetType () == typeof(CreateCharacter)) {
			return this.CreateCharacter ((CreateCharacter)command);
		}
		if (command.GetType () == typeof(AddStoryline)) {
			return this.AddStoryLine ((AddStoryline)command);
		}
		if (command.GetType () == typeof(InitiateDialogue)) {
			return this.InitiateDialogue ((InitiateDialogue)command);
		}
		if (command.GetType () == typeof(AdvanceDialogue)) {
			return this.AdvanceDialogue ((AdvanceDialogue)command);
		}
			
		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(CharacterCreated)){
			this.OnCharacterCreated((CharacterCreated)evt);
		}
		if (evt.GetType () == typeof(AddStorylineAdded)) {
			this.OnStoryLineAdded ((AddStorylineAdded)evt);
		}
		if (evt.GetType () == typeof(DialogueInitiated)) {
			this.OnDialogueInitiated ((DialogueInitiated)evt);
		}
		if (evt.GetType () == typeof(DialogueAdvanced)) {
			this.OnDialogueAdvanced ((DialogueAdvanced)evt);
		}
		if (evt.GetType () == typeof(StorylineCompleted)) {
			this.OnStorylineCompleted ((StorylineCompleted)evt);
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
	private Event[] AddStoryLine(AddStoryline command){
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
			new AddStorylineAdded(command.characterName, command.storylineId, 
				command.introductoryText,playerResponses, command.characterResponses)
		};
	}

	private Event[] InitiateDialogue(InitiateDialogue command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}


		return new Event[] {
			new DialogueInitiated(command.characterName, command.playerId, this.rootNode)
		};

	}

	private Event[] AdvanceDialogue(AdvanceDialogue command){
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
			StorylineCompleted storylineCompleted = new StorylineCompleted (command.characterName, "TEMP-REPLACE WITH STORYLINE ID", command.playerId);
			if (!this.completedStorylineIds.Contains (storylineCompleted.storylineId)) {
				return new Event[]{
					storylineCompleted,
					new StorylinePrizeAwarded(command.playerId, command.characterName,  currentNode.prizeId)
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

		return new Event[]{
			new DialogueAdvanced
			(
				command.characterName, command.playerId, 
				command.input, newNode
			)
		};
	}


	private void OnCharacterCreated(CharacterCreated evt){
		this.id = evt.name;
		this.currentNode = null;
		this.completedStorylineIds = new HashSet<string> ();
	}

	//todo, change this to "story node added". storyline added would insert a new tree into a queue.
	private void OnStoryLineAdded(AddStorylineAdded evt){
		
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

			StoryNode node = rootNode.Find (characterResponse);
			if(node == null) node = new StoryNode(characterResponse);
			//add child should then accept the entry pattern as one argument and the node as the other.
			introductoryTextNode.AddChild (allowedPlayerResponse,node);
		}
	}

	private void OnStorylineCompleted(StorylineCompleted evt){
		completedStorylineIds.Add (evt.storylineId);
	}
		
	private void OnDialogueInitiated(DialogueInitiated evt){
		this.currentNode = rootNode;
	}

	private void OnDialogueAdvanced(DialogueAdvanced evt){
		this.currentNode = evt.newNode;
	}

	private string FormatUserInput(string userInput){
		return userInput.Trim ().ToLower ().Replace (" ", "");
	}
		

	private string[] ConvertPlayerResponsesToRegexes(string[] playerResponses){
		return playerResponses.Select (response => @"\w*"+$"{response}"+@"\w*").ToArray();
	}


}
