using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind XMP metadata facade to the loaded document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Example properties to set
            SetXmpPropertyIfEmpty(xmp, DefaultMetadataProperties.Nickname, "MyDocument");
            SetXmpPropertyIfEmpty(xmp, DefaultMetadataProperties.CreatorTool, "Aspose.Pdf for .NET");
            SetXmpPropertyIfEmpty(xmp, DefaultMetadataProperties.CreateDate, DateTime.UtcNow.ToString("o"));

            // Save the PDF with updated XMP metadata
            // PdfXmpMetadata inherits SaveableFacade, so Save(string) writes the PDF
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Adds the specified XMP property only if it does not already contain a non‑empty value.
    /// Logs a warning when the property already has a value.
    /// </summary>
    static void SetXmpPropertyIfEmpty(PdfXmpMetadata xmp, DefaultMetadataProperties key, string newValue)
    {
        // Check whether the property already exists in the XMP dictionary
        if (xmp.Contains(key))
        {
            // Retrieve the existing value
            XmpValue existing = xmp[key];

            // Simple emptiness check: null or empty string representation
            string existingStr = existing?.ToString() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(existingStr))
            {
                Console.WriteLine($"Warning: XMP property '{key}' already has a non‑empty value ('{existingStr}'). Skipping update.");
                return; // Do not overwrite existing non‑empty value
            }
        }

        // Property is absent or empty – add the new value
        xmp.Add(key, newValue);
    }
}