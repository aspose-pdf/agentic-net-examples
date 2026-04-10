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

        // Load the PDF, flatten it, add custom metadata, and save.
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields and replace them with their values.
            doc.Flatten();

            // Use the PdfFileInfo facade to set a custom metadata entry.
            PdfFileInfo info = new PdfFileInfo();
            info.BindPdf(doc); // Bind the in‑memory document to the facade.

            // Store the flattening date in ISO‑8601 format.
            string flattenDate = DateTime.UtcNow.ToString("o");
            info.SetMetaInfo("FlattenDate", flattenDate);

            // Persist the changes to a new PDF file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved with metadata to '{outputPath}'.");
    }
}