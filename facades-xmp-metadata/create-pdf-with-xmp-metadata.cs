using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_xmp.pdf";

        // Create a new PDF document, add a simple page with text
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));

            // Register the Dublin Core namespace used in the XMP packet
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");

            // Add XMP properties – each value must be wrapped in an XmpValue instance
            doc.Metadata["dc:title"] = new XmpValue("Sample PDF with XMP");
            doc.Metadata["dc:creator"] = new XmpValue("John Doe");
            doc.Metadata["dc:description"] = new XmpValue("Demonstrates XMP metadata binding.");

            // Save the PDF – the XMP block is generated automatically from the dictionary
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with XMP metadata saved to '{outputPath}'.");
    }
}