using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string nickname = "CustomIdentifier";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF, add the Nickname to XMP metadata, and save the updated file
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);                     // Load the PDF into the facade
            xmp.Add("xmp:Nickname", nickname);         // Set the Nickname property
            xmp.Save(outputPdf);                       // Persist changes to a new PDF file
        }

        Console.WriteLine($"Nickname set and saved to '{outputPdf}'.");
    }
}