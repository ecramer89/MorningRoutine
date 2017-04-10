using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//probably will just need the current text it's displaying actually. really dont need anything else.
public class CharacterReadModel : ReadModel {

	public string name;
	public string greeting;
	public CharacterReadModel(int id, string name, string greeting) : base(id){
		this.name = name;
		this.greeting = greeting;
	}
}
