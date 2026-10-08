using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one embedded file (attachment)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the document.");
                return;
            }

            // Update the description of the first embedded file
            FileSpecification fileSpec = doc.EmbeddedFiles[0]; // collections are zero‑based
            fileSpec.Description = "Updated description for the attachment.";

            // Save the modified PDF (same format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment description to '{outputPath}'.");
    }
}
