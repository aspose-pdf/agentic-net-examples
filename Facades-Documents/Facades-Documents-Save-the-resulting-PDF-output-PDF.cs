using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source and the resulting PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Use a Facade (PdfFileMend) to work with the document
        using (PdfFileMend pdfFacade = new PdfFileMend())
        {
            // Load the existing PDF into the facade
            pdfFacade.BindPdf(inputPath);

            // (Optional) Perform modifications here, e.g., add text or images

            // Save the resulting PDF using the facade's Save method
            pdfFacade.Save(outputPath);
        }

        Console.WriteLine($"Resulting PDF saved to '{outputPath}'.");
    }
}