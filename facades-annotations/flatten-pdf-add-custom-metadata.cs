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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all form fields and replace them with their appearance
                doc.Flatten();

                // Attach the PdfFileInfo facade to edit metadata
                using (PdfFileInfo info = new PdfFileInfo(doc))
                {
                    // Custom metadata entry with the flattening date (ISO 8601 UTC)
                    string flattenDate = DateTime.UtcNow.ToString("o");
                    info.SetMetaInfo("FlattenedDate", flattenDate);

                    // Save the updated PDF (metadata changes are written)
                    info.SaveNewInfo(outputPath);
                }
            }

            Console.WriteLine($"Flattened PDF saved with metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}