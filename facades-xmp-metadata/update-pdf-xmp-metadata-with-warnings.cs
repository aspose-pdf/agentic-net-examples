using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfXmpMetadata instance bound to the loaded document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Define the XMP properties you want to set and their new values
            var propertiesToSet = new (DefaultMetadataProperties Key, string NewValue)[]
            {
                (DefaultMetadataProperties.Nickname,    "NewNickname"),
                (DefaultMetadataProperties.CreatorTool, "MyTool 1.0"),
                (DefaultMetadataProperties.BaseURL,     "https://example.com/resources/")
            };

            foreach (var (key, newValue) in propertiesToSet)
            {
                // Check whether the property already exists
                if (xmp.Contains(key))
                {
                    // Retrieve the existing value
                    XmpValue existingValue = xmp[key];

                    // Convert the existing value to string for inspection
                    string existingString = existingValue?.ToString() ?? string.Empty;

                    // If the existing value is non‑empty, log a warning and skip setting
                    if (!string.IsNullOrWhiteSpace(existingString))
                    {
                        Console.WriteLine(
                            $"Warning: XMP property '{key}' already has a non‑empty value '{existingString}'. Skipping update.");
                        continue;
                    }
                }

                // Property is absent or empty – set the new value
                xmp[key] = new XmpValue(newValue);
                Console.WriteLine($"Set XMP property '{key}' to '{newValue}'.");
            }

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
            Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPdf}'.");
        }
    }
}