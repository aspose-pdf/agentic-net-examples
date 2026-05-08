using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfXmpMetadata facade, bind the PDF, modify XMP metadata, and save
        using (PdfXmpMetadata xmpMeta = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmpMeta.BindPdf(inputPdf);

            // ---- Modify standard XMP metadata ----
            // Example: set the Title property (using the XMP Dublin Core namespace)
            xmpMeta.Add("dc:title", new XmpValue("New Document Title"));

            // Example: set the Creator property (using the XMP Dublin Core namespace)
            xmpMeta.Add("dc:creator", new XmpValue("Aspose.Pdf Sample"));

            // ---- Add a custom XMP property ----
            // Register a custom namespace (optional, but recommended for custom fields)
            xmpMeta.RegisterNamespaceURI("cust", "http://example.com/customns/");

            // Add a custom property using the registered prefix
            xmpMeta.Add("cust:Project", new XmpValue("XMP Metadata Demo"));

            // Save the updated PDF with the new XMP metadata
            xmpMeta.Save(outputPdf);

            // Close the facade (optional, as 'using' will dispose it)
            xmpMeta.Close();
        }

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPdf}'.");
    }
}
