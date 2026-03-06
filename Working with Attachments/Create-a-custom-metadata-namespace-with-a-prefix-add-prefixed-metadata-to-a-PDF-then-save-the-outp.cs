using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the XMP metadata dictionary
            Metadata meta = doc.Metadata;

            // Define a custom namespace prefix and URI
            const string prefix = "my";
            const string uri    = "http://example.com/mycustom";

            // Register the custom namespace (prefix → URI)
            meta.RegisterNamespaceUri(prefix, uri);

            // Add a prefixed metadata entry: key uses the prefix, value is a simple string
            meta.Add($"{prefix}:CustomKey", "CustomValue");

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}