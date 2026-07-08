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
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(pdfPath))
            {
                // Create CSV file for the report
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // CSV header
                    writer.WriteLine("Name,Size,Description");

                    // Access embedded files (attachments)
                    var attachments = doc.EmbeddedFiles;
                    if (attachments != null && attachments.Count > 0)
                    {
                        // Use implicit typing to avoid compile‑time dependency on a specific class name
                        foreach (var file in attachments)
                        {
                            // Size is in bytes
                            long size = (long)file.GetType().GetProperty("Size").GetValue(file);

                            // Escape fields that may contain commas or quotes
                            string name = EscapeCsv(file.GetType().GetProperty("Name").GetValue(file) as string);
                            string description = EscapeCsv(file.GetType().GetProperty("Description").GetValue(file) as string);

                            writer.WriteLine($"{name},{size},{description}");
                        }
                    }
                }
            }

            Console.WriteLine($"Attachment report saved to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to escape CSV fields containing commas, quotes, or line breaks
    static string EscapeCsv(string field)
    {
        if (field == null) return string.Empty;
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";
        return field;
    }
}
