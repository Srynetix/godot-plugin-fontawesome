using Godot;
using System.Text.RegularExpressions;

[Tool]
public class FALabel : MarginContainer
{
    [Export(PropertyHint.MultilineText)]
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            UpdateLabel();
        }
    }
    public RichTextLabel Target;

    private const string _pattern = @"\[fa( size=(?<size>16|32))?\](?<name>.*?)\[/fa\]";
    private readonly static MatchEvaluator _evaluator = new MatchEvaluator(Replacer);
    private string _text = "";

    async public override void _Ready()
    {
        Target = new RichTextLabel
        {
            Name = "Label",
            BbcodeEnabled = true,
            FitContentHeight = true
        };
        AddChild(Target);

        await ToSignal(GetTree(), "idle_frame");
        Target.SetAnchorsAndMarginsPreset(LayoutPreset.Wide);
        UpdateLabel();
    }

    private static string Replacer(Match match)
    {
        string iconName = match.Groups["name"].Value;
        string sizeStr = match.Groups["size"].Value;
        var size = sizeStr != "" ? int.Parse(sizeStr) : 16;
        var font = GetOrCreateFAFontForSize(size);

        string iconUnicode = FontAwesomeWrapper.ParseIcon(iconName);
        return "[font=" + font.ResourcePath + "]" + iconUnicode + "[/font]";
    }

    private string ConvertText(string orig)
    {
        return Regex.Replace(orig, _pattern, _evaluator);
    }

    private static DynamicFont GetOrCreateFAFontForSize(int size = 16)
    {
        return (DynamicFont)GD.Load("res://addons/fontawesome/resources/FontAwesome" + size + "px.tres");
    }

    private void UpdateLabel()
    {
        if (Target != null)
        {
            Target.BbcodeText = ConvertText(_text);
        }
    }
}
