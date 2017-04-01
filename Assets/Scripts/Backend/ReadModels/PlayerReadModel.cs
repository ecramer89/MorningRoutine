public class PlayerReadModel : ReadModel {
	public string name;
	public PlayerReadModel(int id, string name) : base(id){
		this.name = name;
	}
}