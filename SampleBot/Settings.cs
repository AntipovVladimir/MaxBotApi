namespace SampleBot;

public class Settings
{
    public long OwnerId { get; set; }
    public long ZabbixReportChatId  { get; set; }

    private const string filename = "settings.json";

    public static Settings Load()
    {
        var obj = JsonHelpers.Load<Settings>(filename);
        return obj ?? new Settings();
    }

    public void Save()
    {
        this.Save(filename);
    }
}