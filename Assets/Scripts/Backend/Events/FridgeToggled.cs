
public class FridgeToggled: Event {

	public int dayId;
	public bool isOpen;

	public FridgeToggled(int dayId, bool isOpen){
		this.dayId = dayId;
		this.isOpen = isOpen;
	}
}
