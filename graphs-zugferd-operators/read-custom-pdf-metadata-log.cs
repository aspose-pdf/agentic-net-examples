using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string logPath = "metadata_audit.log";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Open the log file for appending audit entries
                using (StreamWriter log = new StreamWriter(logPath, append: true))
                {
                    log.WriteLine($"Audit Timestamp: {DateTime.UtcNow:u}");
                    log.WriteLine($"File: {pdfPath}");

                    // Write standard metadata fields
                    log.WriteLine($"Title: {doc.Info.Title}");
                    log.WriteLine($"Author: {doc.Info.Author}");
                    log.WriteLine($"Subject: {doc.Info.Subject}");
                    log.WriteLine($"Keywords: {doc.Info.Keywords}");

                    // Iterate over all metadata entries and log custom ones
                    foreach (var key in doc.Info.Keys)
                    {
                        if (IsStandardKey(key)) continue; // skip already logged standard keys
                        string value = doc.Info[key];
                        log.WriteLine($"Custom Metadata - {key}: {value}");
                    }

                    log.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine($"Metadata audit written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    // Determines whether a metadata key is one of the standard PDF Info entries
    static bool IsStandardKey(string key)
    {
        return string.Equals(key, "Title", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Author", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Subject", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Keywords", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Creator", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Producer", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "CreationDate", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "ModDate", StringComparison.OrdinalIgnoreCase);
    }
}