﻿using System;

public class CharacterReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(CharacterCreated)){
			CharacterCreated characterCreated = (CharacterCreated)evt;
			table.InsertModel (characterCreated.name, new CharacterReadModel(characterCreated.name, characterCreated.greeting));
			return;
		}
		if (type == typeof(StoryInitiated)) {
			StoryInitiated dialogueInitiated = (StoryInitiated)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (dialogueInitiated.characterName);
			character.currentText = dialogueInitiated.initialNode.text;
			character.currentStorylineCompleted = false;
			table.UpdateModel (dialogueInitiated.characterName, character); //not necessary, since we are mutating the reference,
			//but like to be explicit and will soon move to immutable structs for readmodels.
			return;
		}
		if (type == typeof(StoryAdvanced)) {
			StoryAdvanced dialogueAdvanced = (StoryAdvanced)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (dialogueAdvanced.characterName);
			character.currentText = dialogueAdvanced.newNode.text;
			table.UpdateModel (dialogueAdvanced.characterName, character);
			return;
		}
		if (type == typeof(StoryCompleted)) {
			StoryCompleted storylineCompleted = (StoryCompleted)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (storylineCompleted.characterName);
			character.currentText = character.goodbye;
			character.currentStorylineCompleted = true;
			table.UpdateModel (storylineCompleted.characterName, character);
		}
	}

}


