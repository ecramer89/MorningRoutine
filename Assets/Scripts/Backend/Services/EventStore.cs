using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

//probably the easiest way would be to have a global boolean called "streaming" or whatever and then make any ui changes conditional on this being false.
//basically... the ui elements should oly do their thing (dress up the ui) on the very last event of each aggregate
//no, make it a new environment case.
//and think about- whatever you want the state to actually look like, 
//as in, which buttons should appear on the screen or whatever
//the information that determines that 
//needs to be saved to an aggregate that would live on the server
//so for example if
//when the intro is done
//we want a button that says "make coffee" to be appearing on the screen
//then the state changrs that result in that button appearing
//needto be saved to the event stream somehow
//this is what we should save as the game data.
//for each aggregate
//the events that were posted
//and then replay that, at the start of the game, to get the player read model into the state it needs to be 
//and then update the game state (we don't necessarily want the UI events to play through each time we estart the game though, so need some means of)
public class EventStore : Service {

	static Dictionary<string, Dictionary<int, LinkedList<Event>>> data;

	public override void Initialize(){
		data = new Dictionary<string, Dictionary<int, LinkedList<Event>>> ();
		Assembly assembly = Assembly.GetExecutingAssembly ();
		foreach (Type type in assembly.GetTypes()) {
			if (type.BaseType == typeof(ReadModel)) {
				CreateStreamsFor (ModelNameGetter.GetModelName (type));
			}
		}
	}

	public static void CreateStreamsFor(string modelName){
		data.Add (modelName, new Dictionary<int, LinkedList<Event>> ());
	}

	public static LinkedList<Event> CreateStreamFor(Dictionary<int, LinkedList<Event>> modelStreams, int aggregateId){
		LinkedList<Event> newStream = new LinkedList<Event> ();
		modelStreams.Add (aggregateId, newStream);
		return newStream;
	}


	public static LinkedList<Event> GetAllEventsFor(string aggregateType, int aggregateId){
		Dictionary<int, LinkedList<Event>> eventTable;
		if (data.TryGetValue (aggregateType, out eventTable)) {
			LinkedList<Event> events;
			if (eventTable.TryGetValue (aggregateId, out events)) {
				return events;
			}
		}
		return new LinkedList<Event>();
	}

	public static void AppendAllEventsFor (string aggregateType, int aggregateId, Event[] newEvents){
		Dictionary<int, LinkedList<Event>> eventStream;
		if (data.TryGetValue (aggregateType, out eventStream)) {
			LinkedList<Event> events;

			if (!eventStream.TryGetValue (aggregateId, out events)) {
				events = CreateStreamFor (eventStream, aggregateId);
			}

			foreach (Event newEvt in newEvents) {
				events.AddLast (newEvt);
			}
		}

	}
}
