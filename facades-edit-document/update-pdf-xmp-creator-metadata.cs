using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF manipulation
using Aspose.Pdf;           // Core PDF types (required for XmpValue)

// This example reads the XMP metadata from an existing PDF,
// updates the creator field, and writes the changes back to a new PDF file.
class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newCreator = "My New Creator";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the PdfXmpMetadata facade to work with XMP metadata.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the source PDF file.
            xmp.BindPdf(inputPdf);

            // The facade implements IDictionary<string, XmpValue>.
            // Update the creator field (dc:creator) with a new value.
            // XmpValue can be constructed directly from a string.
            xmp["dc:creator"] = new XmpValue(newCreator);

            // Save the PDF with the modified XMP metadata.
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"XMP metadata updated. Output saved to '{outputPdf}'.");
    }
}