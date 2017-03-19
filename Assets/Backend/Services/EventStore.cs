using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

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
