using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timed : MonoBehaviour {
	public float runForSeconds;
	public bool started = false;
	public delegate void TimerDoneHandler();
	public event TimerDoneHandler Done;

	public void Set(int runForSeconds){
		this.runForSeconds = runForSeconds;
	}

	public void On(){
		started = true;
	}


	// Update is called once per frame
	void Update () {
		if (started) {
			runForSeconds -= Time.deltaTime;
			if (runForSeconds < 0) {
				started = false;
				runForSeconds = 0;
				Done ();
			}
		}
	}
}
