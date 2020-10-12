using Godot;

[Tool]
public class IconTouchButton : Panel
{
    public bool Pressed;

    [Export]
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            UpdateLabel();
        }
    }

    private Label _label;
    private string _text = "";
    private int _touchIndex = -1;

    public override void _Ready()
    {
        RectMinSize = new Vector2(96, 96);
        SetAnchorsAndMarginsPreset(LayoutPreset.Wide);

        _label = new Label()
        {
            Align = Label.AlignEnum.Center,
            Valign = Label.VAlign.Center
        };
        _label.AddFontOverride("font", (DynamicFont)GD.Load("res://addons/fontawesome/resources/FontAwesome32px.tres"));
        AddChild(_label);
        _label.SetAnchorsAndMarginsPreset(LayoutPreset.Wide);
        UpdateLabel();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch screenTouch)
        {
            if (_touchIndex == -1 && screenTouch.Pressed && TouchInRect(screenTouch.Position))
            {
                Pressed = true;
                _touchIndex = screenTouch.Index;
            }
            else if (!screenTouch.Pressed && screenTouch.Index == _touchIndex)
            {
                Pressed = false;
                _touchIndex = -1;
            }
        }
    }

    private bool TouchInRect(Vector2 position)
    {
        return new Rect2(RectGlobalPosition, RectSize).HasPoint(position);
    }

    public override void _Process(float delta)
    {
        UpdateLabel();

        if (Pressed)
        {
            Modulate = Colors.DarkGray;
        }
        else
        {
            Modulate = Colors.White;
        }
    }

    private void UpdateLabel()
    {
        if (_label != null)
        {
            _label.Text = FontAwesomeWrapper.ParseIcon(_text);
        }
    }
}
