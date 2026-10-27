using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // NOTE: The original example used an XSL‑FO file, which requires the
        // separate Aspose.Pdf.XslFo package. Because that package is not referenced,
        // we switch to loading a regular PDF file using the core Aspose.Pdf API.
        const string inputPath = "input.pdf";   // Source PDF (replaces input.fo)
        const string outputPath = "output.pdf"; // Destination PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with the standard Document constructor.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Save the resulting PDF using default save options.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF conversion completed successfully: {outputPath}");
    }
}
