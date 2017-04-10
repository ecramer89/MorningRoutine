using System;

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
			
		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(CharacterCreated)){
			this.CharacterCreated((CharacterCreated)evt);
		}
		if (evt.GetType () == typeof(AddStorylineAdded)) {
			this.StoryLineAdded ((AddStorylineAdded)evt);
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

		//todo field validations? entry pattern needs the @, steps must have even length, every second must be form of a regex

		return new Event[] {
			new AddStorylineAdded(command.characterId, command.storylineId, 
				command.parent, command.entryPattern, command.steps, command.text, command.requiredLevel, command.completeFirst)
		};
	}


	private void CharacterCreated(CharacterCreated evt){
		this.id = evt.characterId;
		string greeting = evt.greeting;
		this.dialogueTree = new StoryNode (-1, null, @greeting, greeting);
		this.currentNode = dialogueTree;
	}


	private void StoryLineAdded(AddStorylineAdded evt){
		
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

		StoryNode storyLine = StoryNode.ToStoryNode(evt.storylineId, parent.text, evt.entryPattern, evt.text, evt.steps);
		parent.AddChild (storyLine);
	}
		
}
