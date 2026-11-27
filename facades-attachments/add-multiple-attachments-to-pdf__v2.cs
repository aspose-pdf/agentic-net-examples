using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "attachedMultiple.pdf";

        // Files to be attached
        string[] attachments = { "attachment1.txt", "attachment2.jpg", "attachment3.pdf" };

        // Verify source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePath}");
            return;
        }

        // Verify each attachment exists
        foreach (var att in attachments)
        {
            if (!File.Exists(att))
            {
                Console.Error.WriteLine($"Attachment not found: {att}");
                return;
            }
        }

        try
        {
            // Load the PDF using the high‑level Document API
            Document pdf = new Document(sourcePath);

            // Add each file as an embedded file (attachment)
            foreach (var att in attachments)
            {
                var fileSpec = new FileSpecification(Path.GetFileName(att))
                {
                    Description = Path.GetFileName(att),
                    // The file content must be provided as a stream
                    Contents = new MemoryStream(File.ReadAllBytes(att))
                };
                pdf.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF with the new attachments
            pdf.Save(outputPath);

            Console.WriteLine($"Attachments added successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
