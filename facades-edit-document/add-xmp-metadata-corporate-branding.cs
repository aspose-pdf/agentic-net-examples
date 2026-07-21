using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output_branded.pdf"; // PDF with XMP metadata

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Facade class for XMP manipulation
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPdf);

            // Add corporate branding metadata
            // Keys can be any valid XMP property name; using a custom namespace prefix "xmp"
            xmp.Add("xmp:LogoURL", "https://example.com/logo.png");
            xmp.Add("xmp:BrandColor", "#FF5733"); // hex color code

            // Save the PDF with the new XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}