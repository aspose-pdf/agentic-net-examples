using System;
using System.IO;
using Aspose.Pdf;

class PdfAttachmentReport
{
    // Escapes a CSV field by doubling quotes and surrounding with quotes if needed.
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
        string escaped = field.Replace("\"", "\"\"");
        return mustQuote ? $"\"{escaped}\"" : escaped;
    }

    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Path to the source PDF
        const string csvPath = "attachments_report.csv";    // Output CSV file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("Name,Size,Description");

                // Iterate over all embedded file attachments
                foreach (FileSpecification attachment in doc.EmbeddedFiles)
                {
                    // Attachment name (may be null)
                    string name = attachment.Name ?? string.Empty;

                    // Description (may be null)
                    string description = attachment.Description ?? string.Empty;

                    // Determine size in bytes.
                    long size = 0;
                    if (attachment.Params != null && attachment.Params.Size > 0)
                    {
                        size = attachment.Params.Size;
                    }
                    else if (attachment.Contents != null && attachment.Contents.CanSeek)
                    {
                        size = attachment.Contents.Length;
                    }

                    // Write a CSV line with proper escaping
                    writer.WriteLine($"{EscapeCsv(name)},{size},{EscapeCsv(description)}");
                }
            }
        }

        Console.WriteLine($"Attachment report generated: {csvPath}");
    }
}
