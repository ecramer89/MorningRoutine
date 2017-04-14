using System;

public class CharacterController {
	
	public static ServerResponse CreateCharacter(int characterId, string characterName, string characterGreeting){
		Command command = new CreateCharacter (characterId, characterName, characterGreeting);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterId);
		bool success = CommandHandler.HandleCommand(character, characterId, command);
		ServerResponse response = new ServerResponse (characterId, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}

	public static ServerResponse AddStoryLine(int characterId, int storyLineId, string parentText, string entryPattern, string text,
		StoryNodeData[] steps){
		Command command = new AddStoryline (characterId, storyLineId, parentText, entryPattern, steps, text);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterId);
		bool success = CommandHandler.HandleCommand(character, characterId, command);
		ServerResponse response = new ServerResponse (characterId, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;

	}

	public static ServerResponse InitiateDialogue(int characterId, int playerId){
		Command command = new InitiateDialogue (characterId, playerId);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterId);
		bool success = CommandHandler.HandleCommand(character, characterId, command);
		ServerResponse response = new ServerResponse (characterId, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}


}



