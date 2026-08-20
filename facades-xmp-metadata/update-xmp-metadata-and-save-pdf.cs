using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF manipulation
using Aspose.Pdf;          // Core PDF types (required for XmpValue)

// Example: modify XMP metadata of a PDF and save the result
class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);

        // Modify XMP metadata entries.
        // XmpValue can be created from a string, DateTime, etc.
        // Here we set the creator and title using standard Dublin Core (dc) properties.
        xmp["dc:creator"] = new XmpValue("John Doe");
        xmp["dc:title"]   = new XmpValue("Sample PDF with Updated XMP");

        // Save the PDF with the updated XMP metadata.
        // PdfXmpMetadata inherits from SaveableFacade, so the generic Save method is used.
        try
        {
            xmp.Save(outputPdf);
            Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF with updated XMP metadata: {ex.Message}");
        }
    }
}
