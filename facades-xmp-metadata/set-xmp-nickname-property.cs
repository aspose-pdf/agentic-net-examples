using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfXmpMetadata (a SaveableFacade) to manipulate XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the facade to the existing PDF document
            xmp.BindPdf(inputPdf);

            // Add or replace the xmp:Nickname property with a custom identifier
            xmp.Add(DefaultMetadataProperties.Nickname, "CustomIdentifier");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"XMP Nickname set and PDF saved to '{outputPdf}'.");
    }
}