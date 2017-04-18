using System;

public class CharacterReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(CharacterCreated)){
			CharacterCreated characterCreated = (CharacterCreated)evt;
			table.InsertModel (characterCreated.characterId, new CharacterReadModel(characterCreated.characterId, characterCreated.name, characterCreated.greeting));
			return;
		}
		if (type == typeof(DialogueInitiated)) {
			DialogueInitiated dialogueInitiated = (DialogueInitiated)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (dialogueInitiated.characterId);
			character.currentText = character.greeting;
			table.UpdateModel (dialogueInitiated.characterId, character); //not necessary, since we are mutating the reference,
			//but like to be explicit and will soon move to immutable structs for readmodels.
			return;
		}
		if (type == typeof(DialogueAdvanced)) {
			DialogueAdvanced dialogueAdvanced = (DialogueAdvanced)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (dialogueAdvanced.characterId);
			character.currentText = dialogueAdvanced.newNode.text;
			table.UpdateModel (dialogueAdvanced.characterId, character);
			return;
		}
		if (type == typeof(StorylineCompleted)) {
			StorylineCompleted storylineCompleted = (StorylineCompleted)evt;
			CharacterReadModel character = (CharacterReadModel)table.GetModel (storylineCompleted.characterId);
			character.currentText = "STUB: REPLACE WITH MORE MEANINGFUL TEXT";
			table.UpdateModel (storylineCompleted.characterId, character);
		}
	}

}


