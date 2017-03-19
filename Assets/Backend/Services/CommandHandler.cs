using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;

public class CommandHandler : Service {

	static IEnumerable<Reducer> reducers;

	public override void Initialize(){
		Assembly assembly = Assembly.GetExecutingAssembly();
		reducers = from type in assembly.GetTypes ()
		           where type.BaseType == typeof(Reducer)
			let reducer = (Reducer)Activator.CreateInstance (assembly.GetName().FullName, type.Name).Unwrap ()
		           select reducer;
	}



	public static void HandleCommand(Aggregate aggregate, int aggregateId, Command command){
		LinkedList<Event> events = EventStore.GetAllEventsFor (aggregate.Name, aggregateId);
		foreach(Event evt in events){
			aggregate.hydrate (evt);
		}
		Event[] results = aggregate.execute (command);
		EventStore.AppendAllEventsFor (aggregate.Name, aggregateId, results);
	
		foreach (Reducer reducer in reducers) {
			foreach (Event evt in results) {
				reducer.Reduce (evt, ModelRepository.GetTable(reducer.ModelName));
			}
		}
	}
}
