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

        // Load the PDF, flatten it, add custom metadata, and save.
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields and replace them with their appearance.
            doc.Flatten();

            // Use PdfFileInfo facade to set a custom metadata entry.
            PdfFileInfo info = new PdfFileInfo(doc);
            string flattenDate = DateTime.UtcNow.ToString("o"); // ISO 8601 format
            info.SetMetaInfo("FlattenDate", flattenDate);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}