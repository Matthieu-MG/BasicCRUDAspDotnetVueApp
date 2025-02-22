using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

public class DateOnlyJSONConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.TryGetDateTime(out DateTime dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        var value = reader.GetString();
        if(value == null)
        {
            return default;
        }

        Match match = new Regex("^(\\d\\d)/(\\d\\d)/(\\d\\d\\d\\d)(T|\\s|\\z)").Match(value);
        return match.Success
            ? new DateOnly(int.Parse(match.Groups[3].Value), 
                           int.Parse(match.Groups[2].Value), 
                           int.Parse(match.Groups[1].Value))
            : default;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
}

public class NullableDateOnlyJSONConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out DateTime dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        var value = reader.GetString();
        if (value == null)
        {
            return default;
        }

        Match match = new Regex("^(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)(T|\\s|\\z)").Match(value);
        return match.Success
            ? new DateOnly(int.Parse(match.Groups[3].Value), 
                           int.Parse(match.Groups[2].Value), 
                           int.Parse(match.Groups[1].Value))
            : default;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        => writer.WriteStringValue(value?.ToString("dd/MM/yyyy"));
}

public static class DateOnlyConverterExtensions
{
    public static void AddDateOnlyConverters(this JsonSerializerOptions options)
    {
        options.Converters.Add(new DateOnlyJSONConverter());
        options.Converters.Add(new NullableDateOnlyJSONConverter());
    }
}