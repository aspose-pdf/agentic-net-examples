using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Path to the source PDF
        const string csvPath = "attachments_report.csv";    // Output CSV file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document using the standard constructor (lifecycle rule)
        using (Document doc = new Document(pdfPath))
        {
            // Open a StreamWriter for the CSV report
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // Write CSV header
                writer.WriteLine("Name,Size,Description");

                // Iterate over embedded files (attachments) using reflection to avoid a direct dependency on the concrete type
                foreach (var attachment in doc.EmbeddedFiles)
                {
                    // Retrieve properties via reflection – works for both EmbeddedFile and EmbeddedFileInfo
                    var type = attachment.GetType();
                    var nameProp = type.GetProperty("Name");
                    var sizeProp = type.GetProperty("Size");
                    var descProp = type.GetProperty("Description");

                    string name = EscapeCsv(nameProp?.GetValue(attachment) as string ?? string.Empty);
                    string size = (sizeProp?.GetValue(attachment) is long l) ? l.ToString() : "0"; // Size in bytes
                    string description = EscapeCsv(descProp?.GetValue(attachment) as string ?? string.Empty);

                    writer.WriteLine($"{name},{size},{description}");
                }
            }
        }

        Console.WriteLine($"Attachment report generated: {csvPath}");
    }

    // Helper to escape CSV fields containing commas or quotes
    static string EscapeCsv(string field)
    {
        if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
