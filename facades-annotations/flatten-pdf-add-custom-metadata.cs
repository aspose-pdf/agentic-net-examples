using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Flatten the PDF (remove form fields, keep their appearance)
                doc.Flatten();

                // Create a PdfFileInfo facade bound to the same document
                PdfFileInfo info = new PdfFileInfo(doc);

                // Set a custom metadata entry with the flattening date (ISO 8601 format)
                string flattenDate = DateTime.UtcNow.ToString("o");
                info.SetMetaInfo("FlattenDate", flattenDate);

                // Save the updated PDF with the new metadata
                info.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved with metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}