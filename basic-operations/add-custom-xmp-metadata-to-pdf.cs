using System;
using System.IO;
using Aspose.Pdf;

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

        // Load PDF, add custom XMP metadata, and save preserving metadata
        using (Document doc = new Document(inputPath))
        {
            // Register the Dublin Core namespace (dc) used in the example XMP block
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");

            // Add the desired XMP properties – these will be written into the XMP packet automatically.
            doc.Metadata["dc:creator"] = "John Doe";
            doc.Metadata["dc:title"]   = "Sample PDF";

            // Save the document – metadata is preserved automatically.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom XMP metadata to '{outputPath}'.");
    }
}
