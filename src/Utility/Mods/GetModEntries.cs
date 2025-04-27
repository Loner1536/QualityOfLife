using System.Collections.Generic;
using MelonLoader;

namespace QualityOfLife.Utility.Mods;

public class GetModEntries
{
    public class ModEntry
    {
        public string Category { get; }
        public string DisplayName { get; }
        public string Key { get; }
        public object Value { get; }
        public string Type { get; }

        public ModEntry(string category, string displayName, string key, object value, string type)
        {
            Category = category;
            DisplayName = displayName;
            Key = key;
            Value = value;
            Type = type;
        }
    }

    public static List<ModEntry> Get()
    {
        var results = new List<ModEntry>();
        foreach (var category in MelonPreferences.Categories)
        {
            foreach (var entry in category.Entries)
            {
                var value = entry.BoxedValue;
                var typeName = value?.GetType().Name ?? "Unknown";

                results.Add(new ModEntry(
                    category.Identifier,
                    category.DisplayName,
                    entry.Identifier,
                    value,
                    typeName
                ));
            }
        }
        return results;
    }
}
