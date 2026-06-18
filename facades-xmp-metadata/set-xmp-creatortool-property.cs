using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Current application version to be stored in the XMP CreatorTool property
        string appVersion = "MyApp 1.0.0";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Manipulate XMP metadata using the PdfXmpMetadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPdf);

            // Add or replace the CreatorTool property in the XMP metadata.
            // Use the string overload to avoid needing the Aspose.Pdf.Xmp namespace.
            xmp.Add("CreatorTool", appVersion);

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"CreatorTool set to \"{appVersion}\" and saved to '{outputPdf}'.");
    }
}
