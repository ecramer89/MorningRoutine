using System.Collections;
using System.Collections.Generic;


public struct ServerResponse {
	public bool error;
	public int aggregateId;
	public string modelName;

	public ServerResponse(int aggregateId, string modelName, bool error){
		this.aggregateId = aggregateId;
		this.modelName = modelName;
		this.error = error;
	}
}
