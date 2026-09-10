using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path (will contain the updated XMP metadata)
        const string outputPdf = "output_with_baseurl.pdf";
        // Desired BaseURL value to be stored in the XMP metadata
        const string baseUrl = "https://www.example.com/";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use the PdfXmpMetadata facade to manipulate XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the facade to the existing PDF document
            xmp.BindPdf(inputPdf);

            // Add or replace the BaseURL property.
            // The string overload adds a key/value pair directly.
            // The key must be the full XMP property name.
            xmp.Add("xmp:BaseURL", baseUrl);

            // Save the modified PDF (the original file is not altered)
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"BaseURL metadata added. Output saved to '{outputPdf}'.");
    }
}