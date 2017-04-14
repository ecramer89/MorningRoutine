using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class ModelRepository : Service  {
	static Dictionary<string, ModelTable> repository;

	public override void Initialize(){
		repository = new Dictionary<string, ModelTable> ();
		Assembly assembly = Assembly.GetExecutingAssembly ();
		foreach (Type type in assembly.GetTypes()) {
			if (type.BaseType == typeof(ReadModel)) {
				CreateTable (ModelNameGetter.GetModelName (type));
			}
		}

	}

	public static void CreateTable(string modelName){
		repository.Add (modelName, new ModelTable ());
	}

	public static ModelTable GetTable(string modelName){
		ModelTable table;
		if (repository.TryGetValue (modelName, out table)) {
			return table;
		}
		else throw new KeyNotFoundException ($"Error: no ModelRepository for {modelName}");

	}

	public static ReadModel Get(string modelName, int aggregateId){
		return GetTable (modelName).GetModel (aggregateId);
	}
		
}

public class ModelTable{

	Dictionary<int, ReadModel> entries;

	public ModelTable(){
		entries = new Dictionary<int, ReadModel> ();
	}

	public void InsertModel(int aggregateId, ReadModel initialValue){
	    entries.Add (aggregateId, initialValue);
	}

	public void UpdateModel(int aggregateId, ReadModel update){
		entries [aggregateId] = update;
	}

	public ReadModel GetModel(int aggregateId){
		ReadModel model;
		if (entries.TryGetValue (aggregateId, out model)) {
			return model;
		}
		throw new KeyNotFoundException ($"Error: no model with id {aggregateId}");
	}


}
