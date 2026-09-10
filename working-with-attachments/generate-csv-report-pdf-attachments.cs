using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "attachments_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Create or overwrite the CSV file
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // Write CSV header
                writer.WriteLine("Name,Size,Description");

                // Iterate over all embedded file attachments using reflection to avoid
                // a direct compile‑time dependency on the EmbeddedFile type (which may not
                // be present in some Aspose.Pdf versions).
                foreach (var attachment in doc.EmbeddedFiles)
                {
                    // Retrieve properties via reflection
                    var attType = attachment.GetType();
                    string name = attType.GetProperty("Name")?.GetValue(attachment) as string ?? string.Empty;
                    long size = 0;
                    var sizeObj = attType.GetProperty("Size")?.GetValue(attachment);
                    if (sizeObj != null && long.TryParse(sizeObj.ToString(), out long parsedSize))
                        size = parsedSize;
                    string description = attType.GetProperty("Description")?.GetValue(attachment) as string ?? string.Empty;

                    // Escape fields that may contain commas or quotes
                    name = EscapeCsv(name);
                    description = EscapeCsv(description);

                    writer.WriteLine($"{name},{size},{description}");
                }
            }
        }

        Console.WriteLine($"Attachment report saved to '{csvPath}'.");
    }

    // Helper method to properly escape CSV fields
    static string EscapeCsv(string field)
    {
        if (field.Contains('"'))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            field = $"\"{field}\"";
        return field;
    }
}
