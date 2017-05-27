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


	public static bool HandleCommand(Aggregate aggregate, string aggregateId, Command command){
		string modelName = ModelNameGetter.GetModelName (aggregate.GetType());
	
		try {
			Event[] results = aggregate.execute (command);

			EventStore.AppendAllEventsFor (modelName, aggregateId, results);

			foreach (Event evt in results) {
				aggregate.hydrate (evt);
			}
			//TODO okay, good. so it looks as though each read model does indeed respond to each event.
			//you can make a special read model for each character and have that in addition to the read models for each character, 
			//it doesn't really matter.
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
