using System.Text.Json;

namespace Open5ETools.Core.Common.Helpers;

public static class JsonHelper
{
    public const string MonsterFileName = "5e-SRD-Monsters.json";
    public const string TreasureFileName = "treasures.json";
    public const string SpellFileName = "5e-SRD-Spells.json";

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static IList<T> DeserializeJson<T>(string fileName)
    {
        var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Data/" + fileName) ??
                   throw new FileNotFoundException(fileName);
        return JsonSerializer.Deserialize<IList<T>>(json, Options) ?? [];
    }
}