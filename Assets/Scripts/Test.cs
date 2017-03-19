using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Test : MonoBehaviour {
	
	public static readonly string FAIL = "FAIL";
	public static readonly string SUCCESS = "SUCCESS";
	static int failed = 0;
	static int passed = 0;

	static Func<Exception, bool> RejectException = (Exception e) => { 
		return false;
	};

	static Action<bool, string> ExpectTrue = (bool outcome, string description) => {
		ReportResult(outcome, description);
	};

	static Action<bool, string> ExpectFalse = (bool outcome, string description) => {
		ReportResult(!outcome, description);
	};

	static void ReportResult(bool testPassed, string description){
		string report;
		if (testPassed) {
			passed++;
			report = SUCCESS;
		} else {
			failed++;
			report = FAIL;
		}
		Debug.Log($"\t{description}: {report}");
	}
		
	public struct Assertion{
		string description;
		Func<Event[], bool> handleResult;
		Func<Exception, bool> handleException;
		Action<bool, string> report;
		Action runAfter;

		public Assertion(string description, Func<Event[], bool> handleResult, Func<Exception, bool> handleException, Action<bool, string> report, Action runAfter = null){
			this.description = description;
			this.handleResult = handleResult;
			this.handleException = handleException;
			this.report = report;
			this.runAfter = runAfter;
		}

		public bool run(Event[] actual, Exception exception, bool previousCondition){
			bool outcome = previousCondition;
			outcome = outcome ? outcome && (exception == null ? handleResult (actual) : handleException(exception)) : outcome;
			report (outcome, description);
			if (runAfter != null)
				runAfter ();
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
		Event[] result = null;
		Exception exception = null;
		foreach (Assertion assertion in assertions) {
			try{
			   result = aggregate.execute (command);
			} catch(Exception e){
				exception = e;
			}
			bool previousOutcome = true;
			previousOutcome = assertion.run (result, exception, previousOutcome);
		}
	}

	void Start () {
		if (Environment.Running == Environment.Type.Test) {
			TestEvents ();
			PrintResults ();
		} else
			gameObject.SetActive (false);
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
		Aggregate day = new DayAggregate ();
		return new TestSuite (
			"When creating a day for the first time,",
			day,
			new CreateDay (dayId),
			new Assertion[] {
			new Assertion("It should result in a DayCreatedEvent",
				(Event[] result) => {
					if(result.Length  < 1) return false;
					Event evt = result [0];
					var type = evt.GetType ();
					return evt.GetType () == typeof(DayCreated);
				},
				RejectException,
				ExpectTrue
			),
				new Assertion("It should have a dayId", 
					(Event[] result) => {
						DayCreated dayCreated = (DayCreated)result[0];
						int _dayId = dayCreated.dayId;
						return dayId == _dayId;
					},
					RejectException,
					ExpectTrue,
					()=>{
						day.hydrate(new DayCreated(dayId));
					}
				),
				new Assertion("When creating a day for the second time, it should fail",
					(Event[] result) => {
						return false;
					},
					(Exception e) => {
						return e.GetType() == typeof(ValidationException) && e.Message == "Already exists.";
					},
					ExpectTrue
				)
			}
		);
	}


	void TestMakeCoffee(){


	}
		
}
