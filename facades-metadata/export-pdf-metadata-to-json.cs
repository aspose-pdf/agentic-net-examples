using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfFileInfo facade

// Simple POCO to hold metadata for JSON serialization
public class PdfMetadata
{
    public string? FileName { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Subject { get; set; }
    public string? Keywords { get; set; }
    public string? Creator { get; set; }
    public string? Producer { get; set; }
    // PdfFileInfo returns dates as PDF‑formatted strings, not DateTime objects
    public string? CreationDate { get; set; }
    public string? ModDate { get; set; }
    public int NumberOfPages { get; set; }
    public bool IsEncrypted { get; set; }
    public bool HasOpenPassword { get; set; }
    public bool HasEditPassword { get; set; }
    public bool HasCollection { get; set; }
    // Header is exposed by PdfFileInfo as a Dictionary<string,string>
    public Dictionary<string, string>? Header { get; set; }
    public string? PasswordType { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputFolder = @"C:\PdfFolder";          // folder containing PDFs
        const string outputJson = @"C:\PdfFolder\metadata.json";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        var metadataList = new List<PdfMetadata>();

        // Iterate over all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use PdfFileInfo facade to read metadata
            using (PdfFileInfo info = new PdfFileInfo())
            {
                info.BindPdf(pdfPath); // initialize with the PDF file

                PdfMetadata meta = new PdfMetadata
                {
                    FileName = Path.GetFileName(pdfPath),
                    Title = info.Title,
                    Author = info.Author,
                    Subject = info.Subject,
                    Keywords = info.Keywords,
                    Creator = info.Creator,
                    Producer = info.Producer,
                    // Dates are strings in PDF format, keep them as strings
                    CreationDate = info.CreationDate,
                    ModDate = info.ModDate,
                    NumberOfPages = info.NumberOfPages,
                    IsEncrypted = info.IsEncrypted,
                    HasOpenPassword = info.HasOpenPassword,
                    HasEditPassword = info.HasEditPassword,
                    HasCollection = info.HasCollection,
                    Header = info.Header, // Dictionary<string,string>
                    PasswordType = info.PasswordType.ToString()
                };

                metadataList.Add(meta);
            }
        }

        // Serialize the list to JSON and write to file
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(metadataList, jsonOptions);
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Metadata exported to '{outputJson}'.");
    }
}
