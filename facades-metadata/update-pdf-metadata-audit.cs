using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string csvPath = "metadata_audit.csv";

        // New metadata values – replace with desired values or obtain from arguments
        const string newAuthor = "New Author";
        const string newTitle = "New Title";
        const string newSubject = "New Subject";
        const string newKeywords = "keyword1, keyword2";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Capture original metadata
            string origAuthor = info.Author;
            string origTitle = info.Title;
            string origSubject = info.Subject;
            string origKeywords = info.Keywords;

            // Update metadata
            info.Author = newAuthor;
            info.Title = newTitle;
            info.Subject = newSubject;
            info.Keywords = newKeywords;

            // Save updated PDF to a new file
            bool saved = info.SaveNewInfo(outputPdf);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }

            // Write audit log to CSV
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("Property,Original,New");
                writer.WriteLine($"Author,\"{EscapeCsv(origAuthor)}\",\"{EscapeCsv(newAuthor)}\"");
                writer.WriteLine($"Title,\"{EscapeCsv(origTitle)}\",\"{EscapeCsv(newTitle)}\"");
                writer.WriteLine($"Subject,\"{EscapeCsv(origSubject)}\",\"{EscapeCsv(newSubject)}\"");
                writer.WriteLine($"Keywords,\"{EscapeCsv(origKeywords)}\",\"{EscapeCsv(newKeywords)}\"");
            }

            Console.WriteLine($"Metadata audit written to {csvPath}");
            Console.WriteLine($"Updated PDF saved to {outputPdf}");
        }
    }

    // Helper to escape CSV fields containing quotes or commas
    static string EscapeCsv(string field)
    {
        if (field == null) return string.Empty;
        return field.Replace("\"", "\"\"");
    }
}