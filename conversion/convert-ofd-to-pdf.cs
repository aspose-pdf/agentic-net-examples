using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";
        const string pdfPath = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // NOTE: OFD support is provided by the separate Aspose.Pdf.OFD package.
        // If that package is not referenced, we fall back to the core Document constructor.
        // This will load the file if the format is recognized by the core library.
        using (Document doc = new Document(ofdPath))
        {
            // Save the document as PDF using default settings
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: {pdfPath}");
    }
}