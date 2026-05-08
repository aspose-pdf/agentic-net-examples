using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "generated_with_xmp.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Register the namespaces that will be used in the XMP keys
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
            doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

            // Add custom XMP entries via the metadata indexer
            doc.Metadata["dc:title"] = "Sample PDF with XMP Metadata";
            doc.Metadata["dc:creator"] = "Aspose.Pdf Example";
            // XMP expects dates in ISO‑8601 format
            doc.Metadata["xmp:CreateDate"] = DateTime.UtcNow.ToString("o");

            // Save the PDF – the XMP block is now embedded in the file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with XMP metadata: {outputPath}");
    }
}
