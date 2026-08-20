using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the XMP metadata facade to the loaded document
            using (Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata(doc))
            {
                // Attempt to set XMP properties, logging warnings if they already contain non‑empty values
                SetXmpPropertyIfEmpty(xmp, Aspose.Pdf.Facades.DefaultMetadataProperties.Nickname, "MyDocument");
                SetXmpPropertyIfEmpty(xmp, Aspose.Pdf.Facades.DefaultMetadataProperties.CreatorTool, "MyApp");

                // Save the updated PDF with the new XMP metadata (lifecycle rule: use Save on the facade)
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Helper: set a property only when it is empty; otherwise log a warning
    static void SetXmpPropertyIfEmpty(Aspose.Pdf.Facades.PdfXmpMetadata xmp,
                                      Aspose.Pdf.Facades.DefaultMetadataProperties key,
                                      string newValue)
    {
        // Check whether the property already exists
        if (xmp.Contains(key))
        {
            // Retrieve the existing value
            Aspose.Pdf.XmpValue existing = xmp[key];

            // Consider the value non‑empty if it is not null and not marked as raw (i.e., contains data)
            bool isNonEmpty = existing != null && !existing.IsRaw;

            if (isNonEmpty)
            {
                Console.WriteLine($"Warning: XMP property '{key}' already has a non‑empty value '{existing}'. Skipping update.");
                return;
            }
        }

        // Property is absent or empty – add the new value
        xmp.Add(key, newValue);
        Console.WriteLine($"XMP property '{key}' set to '{newValue}'.");
    }
}