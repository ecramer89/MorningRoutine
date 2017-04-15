using System;
using System.Collections.Generic;


public class CharacterAggregate : Aggregate {

	StoryNode dialogueTree; //the entire tree.
	StoryNode currentNode; //a reference to the specific node in the dialogue tree that the player is currently on.

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
	}


	private Event[] CreateCharacter(CreateCharacter command){
		if (this.id != Aggregate.NullId) {
			throw new ValidationException ("id", "Already exists.");
		}
	
		return new Event[] {
			new CharacterCreated(command.characterId, command.name, command.greeting)
		};

	}

	private Event[] AddStoryLine(AddStoryline command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}
		if (command.steps.Length % 2 != 0) {
			throw new ValidationException ("steps", "Each step requires an entry pattern and a message (length of array should be even- check the text file. Are you missing a field?)");
		}

	
		return new Event[] {
			new AddStorylineAdded(command.characterId, command.storylineId, 
				command.parent, command.entryPattern, command.steps, command.text, command.requiredLevel, command.completeFirst)
		};
	}

	private Event[] InitiateDialogue(InitiateDialogue command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}


		return new Event[] {
			new DialogueInitiated(command.characterId, command.playerId)
		};

	}

	private Event[] AdvanceDialogue(AdvanceDialogue command){
		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Character not found");
		}
		if (this.currentNode == null) {
			throw new ValidationException ("", "Must initiate dialogue.");
		}
		List<StoryNode> children = this.currentNode.GetChildren (command.input);
		if (children == null) {
			throw new ValidationException ("input", $"No response for {command.input}");
		}
		int random = RandomNumberGenerator.Instance.Range(0, children.Count);
		StoryNode newNode = children [random];

		return new Event[]{
			new DialogueAdvanced
			(
				command.characterId, command.playerId, 
				command.input, newNode
			)
		};
	}


	private void OnCharacterCreated(CharacterCreated evt){
		this.id = evt.characterId;
		string greeting = evt.greeting;
		this.dialogueTree = new StoryNode (-1, null, @greeting, greeting);
		this.currentNode = null;
	}


	private void OnStoryLineAdded(AddStorylineAdded evt){
		
		StoryNode parent; 
		if (evt.parent == null || evt.parent.Length == 0) {
			parent = dialogueTree;
		} else {
			//search through dialogue tree until parent is found
			parent = dialogueTree.FindParent(evt.parent);
		}

		//parent not found in the dialogue tree. presume that parent is the root.
		if (parent == null) {
			parent = dialogueTree;
		}

		StoryNodeData[] steps = new StoryNodeData[(evt.steps.Length)/2];
		for (int j = 0, i=0; j < evt.steps.Length - 1; j += 2, i++) {
			string stepEntryPattern = @evt.steps [j];
			string stepText = evt.steps [j + 1];
			StoryNodeData storyNodeData = new StoryNodeData (stepText, stepEntryPattern);
			steps [i] = storyNodeData;
		}

		StoryNode storyLine = StoryNode.ToStoryNode(evt.storylineId, parent.text, evt.entryPattern, evt.text, steps);

		parent.AddChild (storyLine);
	}


	private void OnDialogueInitiated(DialogueInitiated evt){
		this.currentNode = dialogueTree;
	}

	private void OnDialogueAdvanced(DialogueAdvanced evt){
		this.currentNode = evt.newNode;
	}
		
}
