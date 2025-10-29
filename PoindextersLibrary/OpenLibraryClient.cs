using System.Text.Json;

namespace PoindextersLibrary;

public static class OpenLibraryClient
{
    private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://openlibrary.org/") };

    public static async Task<List<Book>> SearchBooksAsync(string query, int limit = LibraryManager.DefaultQueryLimit)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Book>();

        string url = $"search.json?q={Uri.EscapeDataString(query)}&limit={limit}";
        using HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        using Stream stream = await response.Content.ReadAsStreamAsync();
        using JsonDocument document = await JsonDocument.ParseAsync(stream);

        if (!document.RootElement.TryGetProperty("docs", out JsonElement docs) || docs.ValueKind != JsonValueKind.Array)
            return new List<Book>();

        List<Book> results = new List<Book>();
        foreach (JsonElement item in docs.EnumerateArray())
        {
            // Try several alternate fields because OpenLibrary is inconsistent
            //int id = ++LibraryManager.IdCounter;
            string title = GetStringFromAny(item, "title", "title_suggest", "subtitle") ?? string.Empty;
            string author = GetStringFromAny(item, "author_name", "author_alternative_name") ?? string.Empty;
            string publisher = GetStringFromAny(item, "publisher") ?? string.Empty;
            int year = GetIntFromAny(item, "first_publish_year", "publish_year") ?? 0;
            string isbn = GetStringFromAny(item, "isbn", "edition_key") ?? string.Empty; // edition_key or isbn array as fallback

            results.Add(new Book(title, author, publisher, year, isbn));
        }

        return results;
    }

    // returns first non-empty string found for the given property names
    private static string? GetStringFromAny(this JsonElement element, params string[] propertyNames)
    {
        foreach (string name in propertyNames)
        {
            string? value = GetStringFromProperty(element, name);
            if (!string.IsNullOrWhiteSpace(value)) return value;
        }
        return null;
    }

    // handles string, array of strings, number (converted), and returns first meaningful value
    private static string? GetStringFromProperty(JsonElement element, string property)
    {
        if (!element.TryGetProperty(property, out JsonElement p)) return null;

        switch (p.ValueKind)
        {
            case JsonValueKind.String:
                return p.GetString();
            case JsonValueKind.Number:
                return p.GetRawText();
            case JsonValueKind.Array:
                foreach (JsonElement e in p.EnumerateArray())
                {
                    if (e.ValueKind == JsonValueKind.String)
                    {
                        string? s = e.GetString();
                        if (!string.IsNullOrWhiteSpace(s)) return s;
                    }
                    else if (e.ValueKind == JsonValueKind.Number)
                    {
                        return e.GetRawText();
                    }
                }
                break;
        }

        return null;
    }

    // returns first int found from given property names; supports number or array of numbers
    private static int? GetIntFromAny(this JsonElement element, params string[] propertyNames)
    {
        foreach (string name in propertyNames)
        {
            if (!element.TryGetProperty(name, out JsonElement p)) continue;

            if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out int v))
                return v;

            if (p.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement e in p.EnumerateArray())
                {
                    if (e.ValueKind == JsonValueKind.Number && e.TryGetInt32(out int ev))
                        return ev;
                }
            }
        }
        return null;
    }
}