public class PlayerReadModel : ReadModel {
	public string name;
	public PlayerReadModel(string id, string name) : base(id){
		this.name = name;
	}
}