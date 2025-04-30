using System.Collections.Generic;

//detects the first frame a wiimote button is pressed
public class Detection {
    Dictionary<string, bool> lastFrameStates = new();//stores buttons on the wiimote to be pressed
    public bool Get(string name, bool isPressedNow) {
        bool wasPressed = lastFrameStates.TryGetValue(name, out bool prev) && prev;
        lastFrameStates[name] = isPressedNow;
        return isPressedNow && !wasPressed;//if it was not pressed last frame, but is on this frame, then return true
    }
}
