using System.Collections.Generic;


public class Detection {
    Dictionary<string, bool> lastFrameStates = new();
    public bool Get(string name, bool isPressedNow) {
        bool wasPressed = lastFrameStates.TryGetValue(name, out bool prev) && prev;
        lastFrameStates[name] = isPressedNow;
        return isPressedNow && !wasPressed;
    }
}
