using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_xmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Remove XMP metadata.
            // The PdfXmpMetadata facade no longer exposes SetXmpMetadata; the
            // recommended way is to work through the Document.Metadata property.
            // Clearing the Metadata dictionary removes all XMP entries and the
            // XMP stream is omitted when the document is saved.
            // -----------------------------------------------------------------
            if (doc.Metadata != null && doc.Metadata.Count > 0)
            {
                doc.Metadata.Clear();
            }

            // Save the resulting PDF without XMP metadata
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata‑free PDF saved to '{outputPath}'.");
    }
}
