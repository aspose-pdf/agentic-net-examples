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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the PDF actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // CSV header
                writer.WriteLine("Name,Size (bytes),Description");

                // Iterate over all embedded files in the PDF
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    string name = fileSpec.Name ?? string.Empty;
                    string description = fileSpec.Description ?? string.Empty;

                    // Determine the size of the embedded file in bytes
                    long size = 0;
                    if (fileSpec.Contents != null)
                    {
                        Stream content = fileSpec.Contents;
                        if (content.CanSeek)
                        {
                            long originalPos = content.Position;
                            size = content.Length;
                            content.Position = originalPos; // restore position
                        }
                        else
                        {
                            // Fallback: copy to a MemoryStream to get length
                            using (MemoryStream ms = new MemoryStream())
                            {
                                content.CopyTo(ms);
                                size = ms.Length;
                            }
                        }
                    }

                    // Escape commas in fields by surrounding with quotes if needed
                    string escapedName = name.Contains(",") ? $"\"{name}\"" : name;
                    string escapedDesc = description.Contains(",") ? $"\"{description}\"" : description;

                    // Write a CSV line for the current embedded file
                    writer.WriteLine($"{escapedName},{size},{escapedDesc}");
                }
            }
        }

        Console.WriteLine($"Attachment report saved to '{csvPath}'.");
    }
}
