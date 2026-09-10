using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "generated_with_xmp.pdf";

        // Create a new empty PDF document and add a single blank page
        using (Document doc = new Document())
        {
            // Pages collection is 1‑based; add the first page
            doc.Pages.Add();

            // Register the XMP namespaces that will be used
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
            doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

            // Add custom XMP entries via the metadata indexer
            doc.Metadata["dc:creator"] = "John Doe";
            doc.Metadata["dc:title"] = "Sample PDF with XMP Metadata";
            doc.Metadata["xmp:CreateDate"] = DateTime.UtcNow.ToString("o");

            // Save the PDF document – the XMP metadata is persisted automatically
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with XMP metadata: {outputPdf}");
    }
}