

public abstract class ReadModel {
	protected string id;
	public string Id{
		get{
			return id;
		}
	}

	public ReadModel(string id){
		this.id = id;
	}
}
