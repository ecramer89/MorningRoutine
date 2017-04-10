using System;

public class CharacterReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(CharacterCreated)){
			CharacterCreated characterCreated = (CharacterCreated)evt;
			table.InsertModel (characterCreated.characterId, new CharacterReadModel(characterCreated.characterId, characterCreated.name, characterCreated.greeting));
			return;
		}
	}

}


