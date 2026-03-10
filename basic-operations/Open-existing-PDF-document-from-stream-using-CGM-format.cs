using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";   // CGM source file
        const string pdfPath   = "output.pdf";  // Resulting PDF file

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Open the CGM file as a stream and load it with CGM load options.
        using (FileStream cgmStream = File.OpenRead(cgmPath))
        {
            // Default options create an A4 page at 300 dpi.
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            // Load the CGM stream and convert it to a PDF document.
            using (Document pdfDoc = new Document(cgmStream, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"CGM converted to PDF: '{pdfPath}'");
    }
}