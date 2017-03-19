using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Tester : MonoBehaviour {
	public bool on;
	public static string FAIL = "****FAIL****";
	static int failed = 0;
	static int passed = 0;

	static Action<bool> ExpectTrue = (bool outcome) => {
		string report = FAIL;
		if (outcome){
			report = "****SUCCESS****";
			passed++;
		}
		else
			failed++;

		Debug.Log(report);
	};

	static Action<bool> ExpectFalse = (bool outcome) => {
		string report = FAIL;
		if (outcome)
			failed++;
		else{
			report = "****SUCCESS****";
			passed++;
		}

		Debug.Log(report);
	};


	public struct Assertion{
		string description;
		Func<Event[], bool> condition;
		Action<bool> report;

		public Assertion(string description, Func<Event[], bool> condition, Action<bool> report){
			this.description = description;
			this.condition = condition;
			this.report = report;
		}

		public bool run(Event[] actual, bool previousCondition){
			Debug.Log (description);
			bool outcome = previousCondition;
			outcome = outcome ? outcome && condition (actual) : outcome;
			report (outcome);
			return outcome;
		}
	}

	public struct TestSuite
	{
		public Assertion[] assertions;
		public string description;
		public Aggregate aggregate;
		public Command command;

		public TestSuite(string description, Aggregate aggregate, Command command, Assertion[] assertions){
			this.assertions = assertions;
			this.description = description;
			this.aggregate = aggregate;
			this.command = command;
		}

	}

	public void Describe(Aggregate aggregate, Command command, string description, Assertion[] assertions){
		Debug.Log (description);
		Event[] result = aggregate.execute (command);
		bool previousOutcome = true;
		foreach (Assertion assertion in assertions) {
			previousOutcome = assertion.run (result, previousOutcome);
		}
	}

	void Start () {
		if (on) {
			TestEvents ();
			PrintResults ();
		}
	}

	void PrintResults (){
		Debug.Log ($"Ran {passed + failed} tests: ");
		Debug.Log ($"{passed} PASSED");
		Debug.Log ($"{failed} FAILED");
	}

	void TestEvents(){
		TestSuite testDayCreated = TestDayCreated ();
		Describe (testDayCreated.aggregate, testDayCreated.command, testDayCreated.description, testDayCreated.assertions);
	}

	TestSuite TestDayCreated(){

		const int dayId = 0;
		return new TestSuite (
			"When creating a day for the first time,",
			new DayAggregate (),
			new CreateDay (dayId),
			new Assertion[] {
			new Assertion("It should result in a DayCreatedEvent",
				(Event[] result) => {
					if(result.Length  < 1) return false;
					Event evt = result [0];
					var type = evt.GetType ();
						Debug.Log(evt.GetType());
					return evt.GetType () == typeof(DayCreated);
				},
				ExpectTrue
			),
		}
		);
	/*
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
		}*/
	}


	void TestMakeCoffee(){


	}
		
}
