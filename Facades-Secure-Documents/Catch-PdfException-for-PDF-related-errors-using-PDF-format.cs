using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Facade for extracting text from a PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Extract text from all pages
                extractor.ExtractText();

                // Save the extracted text to a file
                extractor.GetText(outputTxt);
            }

            Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
        }
        catch (PdfException pdfEx) // Catch PDF‑related errors
        {
            Console.Error.WriteLine($"PDF error: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
                Console.Error.WriteLine($"Inner exception: {pdfEx.InnerException.Message}");
        }
        catch (Exception ex) // Catch any other unexpected errors
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}