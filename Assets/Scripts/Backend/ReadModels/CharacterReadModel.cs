using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//probably will just need the current text it's displaying actually. really dont need anything else.
public class CharacterReadModel : ReadModel {

	public string name;
	public string greeting;
	public string goodbye;
	public string currentText;
	public bool currentStorylineCompleted;


	public CharacterReadModel(string name, string greeting) : base(name){
		this.name = name;
		this.greeting = greeting;
		this.currentText = "";
		goodbye = "Goodbye";
		currentStorylineCompleted = false;
	}
}
