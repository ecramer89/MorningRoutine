using System;

public class CharacterController {
	
	public static ServerResponse CreateCharacter(string characterName, string characterGreeting){
		Command command = new CreateCharacter (characterName, characterGreeting);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}

	public static ServerResponse AddStoryLine(string characterName, string storyLineId, string introductoryText, string[] playerResponses, string[] characterResponses){
		Command command = new AddStoryline (characterName, storyLineId, introductoryText, playerResponses, characterResponses);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;

	}

	public static ServerResponse InitiateDialogue(string characterName, string playerId){
		Command command = new InitiateDialogue (characterName, playerId);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}

	public static ServerResponse AdvanceDialogue (string characterName, string playerId, string input){
		Command command = new AdvanceDialogue (characterName, playerId, input);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}


}



