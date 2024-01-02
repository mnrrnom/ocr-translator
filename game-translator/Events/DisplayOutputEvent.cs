namespace game_translator.Events;

public class DisplayOutputEvent(string? text = null) : EventArgs
{
    public string? Text { get; } = text;
}