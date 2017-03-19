using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Tester : MonoBehaviour {
	public bool on;
	public static string FAIL = "****FAIL****";


	void Start () {
		if (on) {
			TestEvents ();
		}
	}

	void TestEvents(){
		TestDayCreated ();
	}

	void TestDayCreated(){
		const int dayId = 0;
		Day day = new Day ();

		Debug.Log ("When creating a day for the first time,");
		Event[] result = day.execute (new CreateDay (dayId)); 
		int length = result.Length;
		bool cond = length == 1;
		Debug.Log ($"It should result in one Event: {cond}");
		if (cond) {
			Event evt = result [0];
			var type = evt.GetType ();
			cond = evt.GetType () == typeof(DayCreated);
			Debug.Log ($"It should result in a DayCreatedEvent: {cond}");
			if (cond) {
				DayCreated dayCreated = (DayCreated)evt;
				int _dayId = dayCreated.dayId;
				cond = dayId == _dayId;
				Debug.Log ($"It should have a dayId: {cond}");
			} else {
				Debug.Log ($"{FAIL}: event type is {type}");
			}
		} else {
			Debug.Log ($"{FAIL}: length equals {length}");
		}

		Debug.Log ("When creating a day for the second time,");
		day.hydrate (new DayCreated (dayId));
		Debug.Log ("It should be rejected.");
		try {
		 	day.execute (new CreateDay (dayId)); 
			Debug.Log ("{FAIL}: exception not thrown");
		} catch(ValidationException e){
			Debug.Log ($"True: {e.ToString()}");
		}
		catch(Exception e){
			Debug.Log ("{FAIL}: unknown exception.");
		}
	}


	void TestMakeCoffee(){


	}
		
}
