using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;               // PdfXmpMetadata
using Aspose.Pdf;                       // Document, XmpValue

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input PDF exists – create an empty one if it does not.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' not found. Creating an empty PDF as a placeholder.");
            var placeholder = new Document();
            placeholder.Pages.Add(); // add a single blank page
            placeholder.Save(inputPdf);
        }

        // Define XMP properties to set (qualified XMP key -> new value)
        // Use the full XMP property names (namespace prefix + ':' + property name) as required by Aspose.Pdf.
        var propertiesToSet = new Dictionary<string, string>
        {
            { "dc:title",        "New Document Title" },
            { "xmp:CreatorTool", "My Application" },
            { "dc:description",  "Sample description" }
        };

        // Bind existing PDF for XMP manipulation
        var xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);

        // Iterate over desired properties
        foreach (var kvp in propertiesToSet)
        {
            string key   = kvp.Key;
            string value = kvp.Value;

            // Check if the property already exists and has a non‑empty value
            if (xmp.Contains(key))
            {
                XmpValue existing = xmp[key];
                if (existing != null && !string.IsNullOrWhiteSpace(existing.ToString()))
                {
                    Console.WriteLine(
                        $"Warning: XMP property '{key}' already contains value '{existing}'. Skipping overwrite.");
                    continue; // Skip setting this property
                }
            }

            try
            {
                // Property is absent or empty – add the new value
                xmp.Add(key, new XmpValue(value));
            }
            catch (ArgumentException ex)
            {
                // This will catch invalid keys (e.g., missing namespace prefix)
                Console.WriteLine($"Error: Unable to add XMP property '{key}'. {ex.Message}");
            }
        }

        // Save the PDF with updated XMP metadata
        xmp.Save(outputPdf);

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPdf}'.");
    }
}
