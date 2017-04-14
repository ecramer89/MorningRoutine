using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;


public class AggregateRepository : Service {

	static Dictionary<string, AggregateTable> repository;

	public override void Initialize(){
		repository = new Dictionary<string, AggregateTable> ();
		Assembly assembly = Assembly.GetExecutingAssembly ();
		foreach (Type type in assembly.GetTypes()) {
			if (type.BaseType == typeof(Aggregate)) {
				CreateTable (ModelNameGetter.GetModelName (type));
			}
		}

	}

	public static void CreateTable(string modelName){
		repository.Add (modelName, new AggregateTable ());
	}

	public static AggregateTable GetTable(string modelName){
		AggregateTable table;
		if (repository.TryGetValue (modelName, out table)) {
			return table;
		}
		else throw new KeyNotFoundException ($"Error: no ModelRepository for {modelName}");

	}

	public static Aggregate GetOrCreate(Type aggregateType, int aggregateId){
		string modelName = ModelNameGetter.GetModelName (aggregateType);
		Aggregate aggregate;
		AggregateTable table = GetTable (modelName);
		try{
			aggregate = table.GetAggregate (aggregateId);
		}
		catch(KeyNotFoundException k){
			ConstructorInfo cInfo = aggregateType.GetConstructor (Type.EmptyTypes);
			aggregate = (Aggregate)cInfo.Invoke (Type.EmptyTypes);
			table.InsertAggregate (aggregateId, aggregate);
		}
		return aggregate;
	}

}

public class AggregateTable{

	Dictionary<int, Aggregate> entries;

	public AggregateTable(){
		entries = new Dictionary<int, Aggregate> ();
	}

	public void InsertAggregate(int aggregateId, Aggregate initialValue){
		entries.Add (aggregateId, initialValue);
	}

	public void UpdateAggregate(int aggregateId, Aggregate update){
		entries [aggregateId] = update;
	}

	public Aggregate GetAggregate(int aggregateId){
		Aggregate aggregate;
		if (entries.TryGetValue (aggregateId, out aggregate)) {
			return aggregate;
		}
		throw new KeyNotFoundException ($"Error: no model with id {aggregateId}");
	}


}