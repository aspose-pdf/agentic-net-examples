using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newCreator = "My Application";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);

        // Replace the creator field (dc:creator) if it exists, otherwise add it
        if (xmp.ContainsKey("dc:creator"))
        {
            xmp.Remove("dc:creator");
        }
        xmp.Add("dc:creator", newCreator);

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPdf);
        xmp.Close();

        Console.WriteLine($"Creator field updated and saved to '{outputPdf}'.");
    }
}