using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	public static string Name;
	public enum Type {Test, Production}
	public Type running;
	public static Type Running;

	void Start(){
		Name = gameObject.name;
		Running = running;
	}



}
