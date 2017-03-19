using System;

public class ValidationException : Exception {
	public string field;
	public ValidationException(string field, string msg) : base($"{field}: {msg}"){
		this.field = field;
	}
}
