using Godot;
using Dictionary = Godot.Collections.Dictionary;

public static class FontAwesomeWrapper
{
    private static Dictionary _catalog;
    private static bool _loaded;

    private static void LoadCatalog()
    {
        if (_loaded) return;

        var f = new File();
        var err = f.Open("res://addons/fontawesome/assets/fontawesome-icons.json", File.ModeFlags.Read);
        if (err != Error.Ok)
        {
            GD.PrintErr("Error while loading fontawesome file: ", err);
            return;
        }

        var result = JSON.Parse(f.GetAsText());
        if (result.Error != Error.Ok)
        {
            GD.PrintErr("Error while parsing fontawesome data: ", err);
            return;
        }

        _catalog = (Dictionary)result.Result;
        _loaded = true;
    }

    public static string ParseIcon(string name)
    {
        LoadCatalog();

        // If name is not known, return empty string
        if (!_catalog.Contains(name))
        {
            return "";
        }

        var keyData = (Dictionary)_catalog[name];
        var unicodeStr = (string)keyData["unicode"];
        var unicodeInt = int.Parse(unicodeStr, System.Globalization.NumberStyles.HexNumber);
        return char.ConvertFromUtf32(unicodeInt);
    }
}
