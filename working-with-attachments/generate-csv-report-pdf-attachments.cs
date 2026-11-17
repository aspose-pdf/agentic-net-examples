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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Access the collection of embedded files (attachments)
                var attachments = doc.EmbeddedFiles;

                // Create the CSV file; StreamWriter is also wrapped in a using block
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("Name,Size,Description");

                    // Iterate over each attachment and write its details
                    foreach (FileSpecification fileSpec in attachments)
                    {
                        // Size in bytes – obtain from the underlying stream if available
                        long size = 0;
                        if (fileSpec.Contents != null && fileSpec.Contents.CanSeek)
                        {
                            size = fileSpec.Contents.Length;
                        }

                        // Description may contain commas or quotes; escape them for CSV compliance
                        string description = fileSpec.Description?.Replace("\"", "\"\"") ?? string.Empty;

                        // Enclose fields that may contain commas in double quotes
                        writer.WriteLine($"\"{fileSpec.Name}\",{size},\"{description}\"");
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
}
