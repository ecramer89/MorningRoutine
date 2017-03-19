using System.Collections;
using System.Collections.Generic;

public class EventStore {

	static Dictionary<string, Dictionary<int, LinkedList<Event>>> data;

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
		Dictionary<int, LinkedList<Event>> eventTable;
		if (data.TryGetValue (aggregateType, out eventTable)) {
			LinkedList<Event> events;
			if (eventTable.TryGetValue (aggregateId, out events)) {
				foreach (Event newEvt in newEvents) {
					events.AddLast (newEvt);
				}
			}
		}

	}
}
