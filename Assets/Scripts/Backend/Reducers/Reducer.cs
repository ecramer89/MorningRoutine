using System.Collections.Generic;

public abstract class Reducer {

	string modelName = null;

	public string ModelName {
		get {
			return modelName;
		}
	}

	public Reducer(){
		this.modelName = ModelNameGetter.GetModelName (this.GetType ());
	}

	public abstract void Reduce(Event evt, ModelTable table);
}
