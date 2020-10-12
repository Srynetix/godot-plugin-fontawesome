using Godot;

namespace FontAwesome
{
#if TOOLS
    [Tool]
    public class Plugin : EditorPlugin
    {
        public override void _EnterTree()
        {
            AddCustomType("FALabel", "MarginContainer", (Script)GD.Load("res://addons/fontawesome/nodes/FALabel.cs"), (Texture)GD.Load("res://addons/fontawesome/assets/icon.png"));
            AddCustomType("IconTouchButton", "Panel", (Script)GD.Load("res://addons/fontawesome/nodes/IconTouchButton.cs"), (Texture)GD.Load("res://addons/fontawesome/assets/icon.png"));
        }

        public override void _ExitTree()
        {
            RemoveCustomType("FALabel");
            RemoveCustomType("IconTouchButton");
        }
    }
#endif
}
