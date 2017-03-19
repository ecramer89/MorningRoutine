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


	public static bool HandleCommand(Aggregate aggregate, int aggregateId, Command command){
		string modelName = ModelNameGetter.GetModelName (aggregate.GetType());
		LinkedList<Event> events = EventStore.GetAllEventsFor (modelName, aggregateId);
		foreach (Event evt in events) {
			aggregate.hydrate (evt);
		}
	
		try {
			Event[] results = aggregate.execute (command);

			EventStore.AppendAllEventsFor (modelName, aggregateId, results);

			foreach (Reducer reducer in reducers) {
				foreach (Event evt in results) {
					reducer.Reduce (evt, ModelRepository.GetTable (reducer.ModelName));
				}
			}
			return true;
		} catch (ValidationException e) {
			Debug.Log (e.Message);
		}
		return false;
	}
	

}
