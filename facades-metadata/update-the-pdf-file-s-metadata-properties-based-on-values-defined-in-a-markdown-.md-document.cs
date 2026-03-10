using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    // Simple parser for markdown key/value pairs.
    // Expected format per line: Key: Value
    static Dictionary<string, string> ParseMarkdown(string mdPath)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(mdPath))
        {
            // Skip empty lines and headings
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            int colonIndex = line.IndexOf(':');
            if (colonIndex <= 0)
                continue; // not a key/value line

            string key = line.Substring(0, colonIndex).Trim();
            string value = line.Substring(colonIndex + 1).Trim();
            if (!string.IsNullOrEmpty(key))
                dict[key] = value;
        }
        return dict;
    }

    static void Main()
    {
        const string pdfInputPath = "input.pdf";
        const string pdfOutputPath = "output.pdf";
        const string markdownPath = "metadata.md";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown not found: {markdownPath}");
            return;
        }

        // Parse markdown file for metadata values
        var meta = ParseMarkdown(markdownPath);

        // Use PdfFileInfo facade to modify PDF metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF
            pdfInfo.BindPdf(pdfInputPath);

            // Standard metadata properties
            if (meta.TryGetValue("Title", out var title))
                pdfInfo.Title = title;
            if (meta.TryGetValue("Author", out var author))
                pdfInfo.Author = author;
            if (meta.TryGetValue("Subject", out var subject))
                pdfInfo.Subject = subject;
            if (meta.TryGetValue("Keywords", out var keywords))
                pdfInfo.Keywords = keywords;
            if (meta.TryGetValue("Creator", out var creator))
                pdfInfo.Creator = creator;

            // Dates – PdfFileInfo expects the PDF date string format (yyyyMMddHHmmss)
            if (meta.TryGetValue("CreationDate", out var creationDateStr) &&
                DateTime.TryParse(creationDateStr, out var creationDate))
                pdfInfo.CreationDate = creationDate.ToString("yyyyMMddHHmmss");

            if (meta.TryGetValue("ModDate", out var modDateStr) &&
                DateTime.TryParse(modDateStr, out var modDate))
                pdfInfo.ModDate = modDate.ToString("yyyyMMddHHmmss");

            // Custom metadata (any other keys)
            foreach (var kvp in meta)
            {
                // Skip keys already handled above
                if (kvp.Key.Equals("Title", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("Author", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("Subject", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("Keywords", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("Creator", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("CreationDate", StringComparison.OrdinalIgnoreCase) ||
                    kvp.Key.Equals("ModDate", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Set custom meta info
                pdfInfo.SetMetaInfo(kvp.Key, kvp.Value);
            }

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(pdfOutputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{pdfOutputPath}'.");
    }
}
