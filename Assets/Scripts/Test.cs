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

	static Func<Event[], bool> RejectResult = (Event[] result) => {
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

		public static string FormatShouldPassDescription(string expectation){

			return $"Valid state: it should {expectation}.";
		}

		public static string FormatShouldFailDescription(string reason){
			return $"Invalid state ({reason}): it should fail.";
		}

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

	/*
	 * A TestSuite should verify that a valid command publishes the type type(s) of events, in the right sequence, with the expected fields.
	 * It should also verify that a command DOES NOT succeed if the state of the aggregate on which it is executed invalidates the command.
	 * Since C# is strict about constructor parameters, there is no need to verify that commands with missing fields do not pass (as is necessary in Javascript)
	 * */
	public struct TestSuite
	{
		public Assertion[] assertions;
		public string description;
		public Aggregate aggregate;
		public Command command;

		public static string FormatTestSuiteDescription(string eventName){
			return $"{eventName} tests:";
		}

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
		Debug.Log ("==============================================");
		Debug.Log ($"Ran {passed + failed} tests: ");
		Debug.Log ($"{passed} PASSED; {failed} FAILED");
	}

	void TestEvents(){
		//TestSuite testDayCreated = TestDayCreated ();
		//Describe (testDayCreated.aggregate, testDayCreated.command, testDayCreated.description, testDayCreated.assertions);
	}
	/*	
	 * saved as a sample, but deleted the day aggregate...
	TestSuite TestDayCreated(){
		const int dayId = 0;
		Aggregate aggregate = new DayAggregate ();
		return new TestSuite (
			TestSuite.FormatTestSuiteDescription("DayCreated"),
			aggregate,
			new CreateDay (dayId),
			new Assertion[] {
				new Assertion(
				Assertion.FormatShouldPassDescription("result in a DayCreatedEvent"),
				(Event[] result) => {
					if(result.Length  < 1) return false;
					Event evt = result [0];
					var type = evt.GetType ();
					return evt.GetType () == typeof(DayCreated);
				},
				RejectException,
				ExpectTrue
			),
				new Assertion(
					Assertion.FormatShouldPassDescription("have a dayId"),
					(Event[] result) => {
						DayCreated dayCreated = (DayCreated)result[0];
						int _dayId = dayCreated.dayId;
						return dayId == _dayId;
					},
					RejectException,
					ExpectTrue,
					()=>{
						aggregate.hydrate(new DayCreated(dayId));
					}
				),
				new Assertion(
					Assertion.FormatShouldFailDescription("already exists"),
					RejectResult,
					(Exception e) => {
						return e.GetType() == typeof(ValidationException) && e.Message == "Already exists.";
					},
					ExpectTrue
				)
			}

		);
	}*/




		
}
