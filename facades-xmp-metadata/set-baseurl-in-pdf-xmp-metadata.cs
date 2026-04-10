using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string baseUrl   = "https://www.example.com";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF, set the BaseURL XMP property, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            // Add the standard BaseURL property. Using the string-key overload works for simple values.
            xmp.Add("xmp:BaseURL", baseUrl);
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"BaseURL set and saved to '{outputPdf}'.");
    }
}