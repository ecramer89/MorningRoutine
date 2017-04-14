using System;

public class RandomNumberGenerator : Random {
	static RandomNumberGenerator instance;
	public static RandomNumberGenerator Instance{
		get{
			if (instance == null)
				instance = new RandomNumberGenerator ();
			return instance;
		}
	}
		

	public int Range(int lower, int upper){
		return (int)(Sample () * ((upper-1) - lower)) + lower;
	}
}
