using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "new_with_xmp.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Initialize XMP metadata facade and bind it to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(doc);

                // Register a common namespace (Dublin Core) – optional but recommended
                xmp.RegisterNamespaceURI("dc", "http://purl.org/dc/elements/1.1/");

                // Add standard XMP properties using string keys
                xmp.Add("dc:title", "My New PDF Document");
                xmp.Add("dc:creator", "John Doe");
                xmp.Add("dc:description", "Sample PDF created with Aspose.Pdf and custom XMP metadata.");

                // Add a custom property (no special namespace required)
                xmp.Add("xmp:Nickname", "SampleNick");

                // Save the PDF with the newly created XMP metadata block
                xmp.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with XMP metadata saved to '{outputPdf}'.");
    }
}