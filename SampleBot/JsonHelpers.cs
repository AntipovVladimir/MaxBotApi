using System.Text.Json;

namespace SampleBot;

public static class JsonHelpers
{

    public static T? Load<T>(string filename)
    {
        try
        {
            string cfg = File.ReadAllText(filename);
            if (!string.IsNullOrWhiteSpace(cfg))
            {
                T? obj = JsonSerializer.Deserialize<T>(cfg);
                if (obj is not null) return obj;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        return default;
    }

    private static readonly JsonSerializerOptions jso = new JsonSerializerOptions
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true,
        PropertyNameCaseInsensitive = false
    };


    public static void Save<T>(this T data, string filename)
    {
        try
        {
            string sdata = JsonSerializer.Serialize(data, jso);
            File.WriteAllText(filename, sdata);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}