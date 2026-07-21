using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "XmpMetadataSample.pdf";

        // Create a new PDF document and add a simple page with text
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a text fragment to the page
            TextFragment tf = new TextFragment("Hello, XMP metadata!");
            page.Paragraphs.Add(tf);

            // Register the namespaces that will be used in the XMP packet
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
            doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

            // Add custom XMP metadata entries (values can be set directly via the indexer)
            doc.Metadata["dc:creator"] = "John Doe";
            doc.Metadata["dc:title"] = "Sample PDF with XMP";
            doc.Metadata["xmp:CreateDate"] = DateTime.UtcNow.ToString("o");

            // Save the PDF – the XMP metadata is automatically embedded
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with XMP metadata saved to '{outputPath}'.");
    }
}