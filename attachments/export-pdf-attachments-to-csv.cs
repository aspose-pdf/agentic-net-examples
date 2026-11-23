using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "attachments.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF and open the CSV writer in nested using blocks for proper disposal
        using (Document doc = new Document(pdfPath))
        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            writer.WriteLine("Name,Description");

            // Iterate over embedded files using FileSpecification (the correct type)
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                string name = EscapeCsv(fileSpec.Name);
                string description = EscapeCsv(fileSpec.Description ?? string.Empty);
                writer.WriteLine($"{name},{description}");
            }
        }

        Console.WriteLine($"Attachment list written to '{csvPath}'.");
    }

    // Helper to escape CSV fields containing commas, quotes or newlines
    static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            field = $"\"{field}\"";

        return field;
    }
}