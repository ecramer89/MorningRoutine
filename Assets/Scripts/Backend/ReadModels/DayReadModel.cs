
public class DayReadModel : ReadModel {
	int id;
	public int Id{
		get{
			return id;
		}
	}

	public DayReadModel(int id){
		this.id = id;
	}
}
