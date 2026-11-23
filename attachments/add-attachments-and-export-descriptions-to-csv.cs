using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // PDF to which attachments will be added
        const string outputPdfPath = "output_with_attachments.pdf";
        const string csvPath = "attachments.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Add attachments (FileSpecification) to the document
            // -----------------------------------------------------------------
            // Example attachment 1
            var attachment1 = new FileSpecification("attachment1.txt", "First attachment description");
            // Description can also be set after construction if needed
            attachment1.Description = "First attachment description";
            doc.EmbeddedFiles.Add(attachment1);

            // Example attachment 2
            var attachment2 = new FileSpecification("attachment2.jpg", "Second attachment description");
            attachment2.Description = "Second attachment description";
            doc.EmbeddedFiles.Add(attachment2);

            // Save the PDF with the new attachments
            doc.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // Re-open the PDF (now containing attachments) to enumerate them
        // -----------------------------------------------------------------
        using (Document doc = new Document(outputPdfPath))
        {
            // Open CSV for writing
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FileName,Description");

                // Enumerate each embedded file and write its description
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // FileSpecification.Name contains the original file name
                    string fileName = Path.GetFileName(fileSpec.Name);
                    string description = fileSpec.Description ?? string.Empty;

                    // Escape double quotes and wrap fields in quotes to handle commas
                    fileName = $"\"{fileName.Replace("\"", "\"\"")}\"";
                    description = $"\"{description.Replace("\"", "\"\"")}\"";

                    writer.WriteLine($"{fileName},{description}");
                }
            }
        }

        Console.WriteLine($"Attachments enumerated and written to CSV: {csvPath}");
    }
}
