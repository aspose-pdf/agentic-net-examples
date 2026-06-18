using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";
        const string metaKey = "FlattenedDate";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, flatten it, and add custom metadata.
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields and replace them with their appearance.
            doc.Flatten();

            // Use PdfFileInfo facade to set a custom metadata entry.
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                string dateValue = DateTime.UtcNow.ToString("o"); // ISO 8601 format
                info.SetMetaInfo(metaKey, dateValue);

                // Save the updated document with the new metadata.
                info.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"Flattened PDF saved with metadata to '{outputPath}'.");
    }
}