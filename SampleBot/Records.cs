namespace SampleBot;

/// <summary>
/// Событие от заббикса
/// </summary>
/// <param name="To">ALERT.SENDTO</param>
/// <param name="Subject">ALERT.SUBJECT</param>
/// <param name="Message">ALERT.MESSAGE</param>
public record EventUpdate(string To, string Subject, string Message);