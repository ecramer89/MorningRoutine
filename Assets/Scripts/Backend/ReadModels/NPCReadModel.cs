using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReadModel : ReadModel {

	public string name;
	public string greeting;
	public NPCReadModel(int id, string name, string greeting) : base(id){
		this.name = name;
		this.greeting = greeting;
	}
}
