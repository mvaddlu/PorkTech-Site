using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public partial record BusinessForm
{
    [GeneratedRegex(@"/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/")]
    public static partial Regex EmailRegex();

    [GeneratedRegex(@"/^(\+?1\s*[-.\s]*)?(\(\d{3}\)|\d{3})[-.\s]*\d{3}[-.\s]*\d{4}$/")]
    public static partial Regex PhoneRegex();

    public string? BusinessName { get; init; }
    public string? BusinessType { get; init; }
    public string? ContactEmail { get; init; }
    public string? ContactPhone { get; init; }
}