using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // ZUGFeRD attachments are typically named "ZUGFeRD-invoice.xml"
            const string zugFerdName = "ZUGFeRD-invoice.xml";

            // Attempt to delete the ZUGFeRD attachment; ignore if it does not exist
            try
            {
                doc.EmbeddedFiles.Delete(zugFerdName);
            }
            catch (Exception ex)
            {
                // Log but continue – the attachment may simply be absent
                Console.WriteLine($"Could not delete attachment '{zugFerdName}': {ex.Message}");
            }

            // Save the modified PDF, preserving all other content
            doc.Save(outputPath);
        }

        Console.WriteLine($"ZUGFeRD attachment removed (if present). Output saved to '{outputPath}'.");
    }
}