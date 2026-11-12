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

        // Load XMP metadata using the correct Facade class
        var xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // -------------------------------------------------------------------
        // 1. Built‑in XMP properties (use string keys)
        // -------------------------------------------------------------------
        var builtInProperties = new (string Key, string Value)[]
        {
            ("Title", "New Document Title"),
            ("CreatorTool", "John Doe")
        };

        foreach (var (key, value) in builtInProperties)
        {
            if (xmp.Contains(key))
            {
                // Retrieve the existing value for the warning message
                xmp.TryGetValue(key, out XmpValue existing);
                Console.WriteLine($"Warning: XMP property '{key}' already has a non‑empty value '{existing}'. It will be overwritten.");
                xmp.Remove(key);
            }
            // Add the new value (PdfXmpMetadata accepts string value directly)
            xmp.Add(key, value);
        }

        // -------------------------------------------------------------------
        // 2. Custom XMP property (requires namespace registration)
        // -------------------------------------------------------------------
        const string customPrefix = "ex";
        const string customUri = "http://example.com/ns/1.0/";
        xmp.RegisterNamespaceURI(customPrefix, customUri);
        string customKey = $"{customPrefix}:MyProperty";
        if (xmp.Contains(customKey))
        {
            xmp.TryGetValue(customKey, out XmpValue existing);
            Console.WriteLine($"Warning: XMP custom property '{customKey}' already has a non‑empty value '{existing}'. It will be overwritten.");
            xmp.Remove(customKey);
        }
        xmp.Add(customKey, "Custom Value");

        // -------------------------------------------------------------------
        // 3. Write the updated XMP packet back into a new PDF file
        // -------------------------------------------------------------------
        using (var doc = new Document(inputPath))
        {
            // Save the XMP packet to a memory stream and attach it to the document
            using (var ms = new MemoryStream())
            {
                xmp.Save(ms);          // writes the XMP packet
                ms.Position = 0;       // reset for reading
                doc.SetXmpMetadata(ms);
            }
            doc.Save(outputPath);
        }

        xmp.Close();
        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}
