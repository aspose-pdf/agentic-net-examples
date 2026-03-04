using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for any additional facades if needed (not used here)

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";   // CGM source file
        const string pdfPath   = "output.pdf";  // Destination PDF file

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

            // Document(Stream, LoadOptions) converts the CGM stream to a PDF document.
            using (Document doc = new Document(cgmStream, loadOptions))
            {
                // Save the resulting PDF.
                doc.Save(pdfPath);
                Console.WriteLine($"CGM converted to PDF: '{pdfPath}'");
            }
        }
    }
}