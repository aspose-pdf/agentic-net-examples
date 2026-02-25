using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input MHT file and output PDF file
        const string inputMhtPath = "input.mht";
        const string outputPdfPath = "output.pdf";

        // Verify that the source MHT file exists
        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{inputMhtPath}'.");
            return;
        }

        // Initialize load options for MHT files
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Load the MHT document and automatically convert it to a PDF document
        using (Document pdfDocument = new Document(inputMhtPath, mhtLoadOptions))
        {
            // Save the resulting document as a standard PDF (PDF/A is not enforced)
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPdfPath}'.");
    }
}