using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document class

class Program
{
    static void Main()
    {
        // Input PDF file containing attachments
        const string pdfPath = "input.pdf";

        // Output CSV file that will hold the report
        const string csvPath = "attachments_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document doc = new Document(pdfPath))
            {
                // Open a StreamWriter for the CSV output
                using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("Name,Size (bytes),Description");

                    // Helper to escape commas and quotes for CSV fields
                    string Escape(string s) => s.Contains(",") ? $"\"{s.Replace("\"", "\"\"")}\"" : s;

                    // Iterate over all embedded files (attachments) in the PDF using reflection
                    foreach (var embedded in doc.EmbeddedFiles)
                    {
                        var type = embedded.GetType();
                        string name = type.GetProperty("Name")?.GetValue(embedded) as string ?? string.Empty;
                        long size = (long?)type.GetProperty("FileSize")?.GetValue(embedded) ?? 0L;
                        string description = type.GetProperty("Description")?.GetValue(embedded) as string ?? string.Empty;

                        writer.WriteLine($"{Escape(name)},{size},{Escape(description)}");
                    }
                }

                Console.WriteLine($"Attachment report saved to '{csvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
