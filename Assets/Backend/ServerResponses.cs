using System.Collections;
using System.Collections.Generic;


public struct CreateDayResponse {
	public bool error;
	public int dayId;

	public CreateDayResponse(int dayId, bool error){
		this.dayId = dayId;
		this.error = error;
	}

}
