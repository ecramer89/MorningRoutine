using System.Collections;
using System.Collections.Generic;


public struct ServerResponse {
	public bool error;
	public string aggregateIdentifier;
	public string modelName;

	public ServerResponse(string aggregateIdentifier, string modelName, bool error){
		this.aggregateIdentifier = aggregateIdentifier;
		this.modelName = modelName;
		this.error = error;
	}
}
