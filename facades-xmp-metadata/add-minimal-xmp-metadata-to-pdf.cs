using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_xmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine whether any XMP metadata is already present.
            // The Metadata collection is empty when no XMP packet exists.
            bool hasMetadata = doc.Metadata.Count > 0;

            if (!hasMetadata)
            {
                // Register the standard namespaces we are going to use.
                doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
                doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

                // Add a minimal set of XMP properties.
                doc.Metadata["dc:title"] = "Untitled Document";
                doc.Metadata["xmp:CreatorTool"] = "Aspose.Pdf";
                doc.Metadata["xmp:ModifyDate"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

                // Save the PDF with the newly created XMP metadata.
                doc.Save(outputPath);
                Console.WriteLine($"Minimal XMP metadata added and saved to '{outputPath}'.");
            }
            else
            {
                // Metadata already exists – simply copy the original PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Existing XMP metadata preserved; saved to '{outputPath}'.");
            }
        }
    }
}
