using game_translator.Events;
using SharpHook;
using SharpHook.Native;

namespace game_translator;

public class KeyboardHookHandler
{
    private readonly TaskPoolGlobalHook _hook = new ();
    private bool _altPressed;
    public event EventHandler<OnKeyboardHookTriggeredEvent>? OnTriggered;

    public void Dispose()
    {
        _hook.Dispose();
    }

    public async Task ActivateAsync()
    {
        _hook.KeyPressed += (_, args) =>
        {
            if (args.Data.KeyCode == KeyCode.VcLeftAlt)
                _altPressed = true;
            else if (args.Data.KeyCode == KeyCode.VcBackQuote && _altPressed)
                OnTriggered?.Invoke(this, new ());
        };

        _hook.KeyReleased += (_, args) =>
        {
            if (args.Data.KeyCode == KeyCode.VcLeftAlt)
            {
                _altPressed = false;
            }
        };
        
        await _hook.RunAsync();
    }
}