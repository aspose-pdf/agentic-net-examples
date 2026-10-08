using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "generated.pdf";

        // Create a simple PDF with one blank page
        Document doc = new Document();
        doc.Pages.Add(); // ensure the PDF has at least one page

        // Prepare XMP metadata and add a timestamp property
        string timestamp = DateTime.UtcNow.ToString("o"); // ISO‑8601 format

        // Register a custom namespace (optional, you can use any prefix you like)
        doc.Metadata.RegisterNamespaceUri("my", "http://example.com/custom");

        // Standard XMP property
        doc.Metadata["xmp:CreateDate"] = timestamp;
        // Custom property in our namespace
        doc.Metadata["my:Timestamp"] = timestamp;

        // Save the PDF – the XMP metadata is embedded automatically
        doc.Save(pdfPath);

        Console.WriteLine($"PDF generated with timestamp metadata at '{pdfPath}'.");
    }
}
