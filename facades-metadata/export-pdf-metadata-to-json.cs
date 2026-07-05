using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Globalization;
using Aspose.Pdf.Facades;

class Program
{
    // Simple DTO to hold PDF metadata. All reference‑type properties are nullable to satisfy the non‑nullable warnings.
    private class PdfMetadata
    {
        public string? FileName { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Subject { get; set; }
        public string? Keywords { get; set; }
        public string? Creator { get; set; }
        public string? Producer { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int NumberOfPages { get; set; }
        public bool IsEncrypted { get; set; }
        public bool HasOpenPassword { get; set; }
        public bool HasEditPassword { get; set; }
    }

    static void Main()
    {
        const string inputFolder = "PdfFolder";          // folder containing PDFs
        const string outputJson = "metadata.json";       // output JSON file

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        var allMetadata = new List<PdfMetadata>();

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Use PdfFileInfo facade to read metadata
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(pdfPath); // load the PDF

                    PdfMetadata meta = new PdfMetadata
                    {
                        FileName = Path.GetFileName(pdfPath),
                        Title = info.Title,
                        Author = info.Author,
                        Subject = info.Subject,
                        Keywords = info.Keywords,
                        Creator = info.Creator,
                        Producer = info.Producer,
                        CreationDate = ParsePdfDate(info.CreationDate),
                        ModDate = ParsePdfDate(info.ModDate),
                        NumberOfPages = info.NumberOfPages,
                        IsEncrypted = info.IsEncrypted,
                        HasOpenPassword = info.HasOpenPassword,
                        HasEditPassword = info.HasEditPassword
                    };

                    allMetadata.Add(meta);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        // Serialize the list to JSON and write to file
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(allMetadata, jsonOptions);
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Metadata of {allMetadata.Count} PDF(s) written to '{outputJson}'.");
    }

    /// <summary>
    /// Parses a PDF date string (format "yyyyMMddHHmmss" or "yyyyMMddHHmmsszzz") into a nullable DateTime.
    /// Returns null if the input is null, empty, or cannot be parsed.
    /// </summary>
    private static DateTime? ParsePdfDate(string? pdfDate)
    {
        if (string.IsNullOrWhiteSpace(pdfDate))
            return null;

        // Aspose.Pdf stores dates in PDF date format: "yyyyMMddHHmmss" optionally followed by timezone offset.
        // Try the basic format first, then the extended format with timezone.
        string[] formats = { "yyyyMMddHHmmss", "yyyyMMddHHmmsszzz" };
        if (DateTime.TryParseExact(pdfDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime dt))
            return dt;

        // Fallback to generic parsing – may succeed for some locale‑specific strings.
        if (DateTime.TryParse(pdfDate, out dt))
            return dt;

        return null; // could not parse
    }
}
