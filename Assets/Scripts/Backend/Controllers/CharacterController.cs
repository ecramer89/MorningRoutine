using System;

public class CharacterController {
	
	public static ServerResponse CreateCharacter(string characterName, string characterGreeting){
		Command command = new CreateCharacter (characterName, characterGreeting);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}

	public static ServerResponse AddNodeToStory(string characterName, string storyLineId, string introductoryText, string[] playerResponses, string[] characterResponses, Event[] eventsToPublishOnReaching = null){
		Command command = new AddNodeToStory (characterName, storyLineId, introductoryText, playerResponses, characterResponses, eventsToPublishOnReaching);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;

	}

	public static ServerResponse InitiateStory(string characterName, string playerId){
		Command command = new InitiateStory (characterName, playerId);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}

	public static ServerResponse AdvanceStory (string characterName, string playerId, string input){
		Command command = new AdvanceStory (characterName, playerId, input);
		Aggregate character = AggregateRepository.GetOrCreate (typeof(CharacterAggregate), characterName);
		bool success = CommandHandler.HandleCommand(character, characterName, command);
		ServerResponse response = new ServerResponse (characterName, ModelNameGetter.GetModelName(character.GetType()), !success);
		return response;
	}


}



