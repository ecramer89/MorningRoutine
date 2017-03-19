using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;


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
		
}

public class ModelTable{

	Dictionary<int, ReadModel> entries;

	public ModelTable(){
		entries = new Dictionary<int, ReadModel> ();
	}

	public void CreateOne(int aggregateId, ReadModel initialValue){
	    entries.Add (aggregateId, initialValue);
	}


}
