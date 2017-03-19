using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Initialize : MonoBehaviour {

	void Start () {
		Assembly assembly = Assembly.GetExecutingAssembly();
		foreach(Type type in assembly.GetTypes ()){
			if(type.BaseType == typeof(Service)){
				Service service = (Service)Activator.CreateInstance (assembly.GetName ().FullName, type.Name).Unwrap ();
				service.Initialize ();
			}
		}

		CommandHandler.test ();

	}
}
