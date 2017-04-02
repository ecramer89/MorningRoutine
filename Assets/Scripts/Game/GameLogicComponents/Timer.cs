using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public float runForSeconds;
	public bool started = false;
	public delegate void TimerDoneHandler();
	public event TimerDoneHandler TimerDone;

	public void SetTimer(int runForSeconds){
		this.runForSeconds = runForSeconds;
	}

	public void StartTimer(){
		started = true;
	}


	// Update is called once per frame
	void Update () {
		if (started) {
			runForSeconds -= Time.deltaTime;
			if (runForSeconds < 0) {
				TimerDone ();
			}
		}
	}
}
