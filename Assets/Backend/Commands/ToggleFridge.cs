

public class ToggleFridge : Command {
	public int dayId;
	public bool isOpen;

	public ToggleFridge(int dayId, bool isOpen){
		this.dayId = dayId;
		this.isOpen = isOpen;
	}

}
