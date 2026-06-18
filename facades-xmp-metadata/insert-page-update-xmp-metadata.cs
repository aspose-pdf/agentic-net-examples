using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF, insert a new page, update XMP metadata, then save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Insert a blank page at the end of the document.
            pdfDoc.Pages.Add();

            // Bind XMP metadata handler to the same document instance.
            PdfXmpMetadata xmpMeta = new PdfXmpMetadata(pdfDoc);

            // Add or replace a standard XMP property.
            xmpMeta.Add("dc:title", "Updated Document Title");

            // Register a custom namespace prefix and add a custom property.
            xmpMeta.RegisterNamespaceURI("my", "http://example.com/ns");
            xmpMeta.Add("my:customProperty", "CustomValue");

            // Save the modified PDF (including the new page and XMP changes).
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with new page and updated XMP metadata to '{outputPdfPath}'.");
    }
}