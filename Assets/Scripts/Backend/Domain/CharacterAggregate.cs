using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterAggregate : Aggregate {

	StoryNode dialogueTree; //the entire tree.
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

		for(int i=0;i<command.playerResponses.Length; i++){
			string playerResponse = command.playerResponses [i];
			string characterResponse = command.characterResponses [i];
			if (playerResponse.Length == 0 || characterResponse.Length == 0) {
				throw new ValidationException ("response", $"Response can't be empty; check the values for response {i+1}");
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
			new DialogueInitiated(command.characterName, command.playerId)
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
		if (this.currentNode.IsRoot ()) {
			StorylineCompleted storylineCompleted = new StorylineCompleted (command.characterName, this.currentNode.id, command.playerId);
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
		string greeting = evt.greeting;
		this.dialogueTree = new StoryNode (Constants.NULL_ID, greeting);
		this.currentNode = null;
		this.completedStorylineIds = new HashSet<string> ();
	}


	private void OnStoryLineAdded(AddStorylineAdded evt){
		
		StoryNode introductoryTextNode; 
		if (evt.introductoryText == null || evt.introductoryText.Length == 0) {
			introductoryTextNode = dialogueTree;
		} else {
			//search through dialogue tree until parent is found
			//when we create 
			introductoryTextNode = dialogueTree.FindParent(evt.introductoryText);
		}

		//parent not found in the dialogue tree. presume that parent is the root.
		if (introductoryTextNode == null) {
			introductoryTextNode = dialogueTree;
		}
			
		for (int i=0;i<evt.playerResponses.Length;i++) {
			string allowedPlayerResponse = evt.playerResponses [i];
			string characterResponse = evt.characterResponses [i];
			//TODO shouldn't it check to see if there is already an existing node matching the given charcter response?
			//instead of making a new one. because if there is, we should add it as a child of this node. (make a densely interconnected graph)

			StoryNode node = dialogueTree.FindParent (characterResponse);
			if(node == null) node = new StoryNode(evt.storylineId, characterResponse);
			//add child should then accept the entry pattern as one argument and the node as the other.
			introductoryTextNode.AddChild (allowedPlayerResponse,node);
		}
	}

	private void OnStorylineCompleted(StorylineCompleted evt){
		completedStorylineIds.Add (evt.storylineId);
	}
		
	private void OnDialogueInitiated(DialogueInitiated evt){
		this.currentNode = dialogueTree;
	}

	private void OnDialogueAdvanced(DialogueAdvanced evt){
		this.currentNode = evt.newNode;
	}

	private string FormatUserInput(string userInput){
		return userInput.Trim ().ToLower ().Replace (" ", "");
	}
		

	private string[] ConvertPlayerResponsesToRegexes(string[] playerResponses){
		return playerResponses.Select (response => @"\\w*" +$"{response}\\w*").ToArray();
	}



		
}
