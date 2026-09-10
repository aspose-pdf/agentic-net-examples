using System;
using Aspose.Pdf.Facades;          // Facade API for XMP manipulation
using Aspose.Pdf;                 // Needed for DefaultMetadataProperties enum

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Manipulate XMP metadata using the PdfXmpMetadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the facade to the existing PDF document
            xmp.BindPdf(inputPdf);

            // Remove the Nickname element from the XMP metadata
            // Two overloads are available; using the enum version is type‑safe
            xmp.Remove(DefaultMetadataProperties.Nickname);

            // Save the modified PDF to a new file
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Nickname element removed. Output saved to '{outputPdf}'.");
    }
}