using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade, remove the Nickname element, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF document into the facade.
            xmp.BindPdf(inputPdf);

            // Remove the Nickname element using the enum key.
            xmp.Remove(DefaultMetadataProperties.Nickname);
            // Alternatively, you could use the string key:
            // xmp.Remove("xmp:Nickname");

            // Save the modified PDF (the XMP changes are written back to the file).
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Nickname element removed. Output saved to '{outputPdf}'.");
    }
}