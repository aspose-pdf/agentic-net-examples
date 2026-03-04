using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Open the CGM file as a stream and load it using CgmLoadOptions.
        using (FileStream cgmStream = File.OpenRead(cgmPath))
        {
            CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 page size, 300 DPI
            using (Document doc = new Document(cgmStream, loadOptions))
            {
                // Save the resulting PDF.
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Converted CGM to PDF: {outputPdf}");
    }
}