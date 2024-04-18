using Newtonsoft.Json;

public record SmtpData
{
    [JsonProperty("smtp_server")]
    public string SmtpServer { get; init; } = "smtp.gmail.com";
    [JsonProperty("smtp_port")]
    public int SmtpPort { get; init; } = 465;
    [JsonProperty("smtp_username")]
    public string SmtpUsername { get; init; } = string.Empty;
    [JsonProperty("smtp_password")]
    public string SmtpPassword { get; init; } = string.Empty;
}