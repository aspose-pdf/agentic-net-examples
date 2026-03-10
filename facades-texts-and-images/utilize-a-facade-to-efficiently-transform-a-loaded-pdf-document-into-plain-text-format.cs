using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output plain text file path
        const string outputTxtPath = "output.txt";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Use PdfExtractor (a Facade) to extract text.
        // The Facade implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor.
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding (default is Unicode).
            // This prepares the extractor for retrieving text.
            extractor.ExtractText(Encoding.Unicode);

            // Save the extracted text to a plain .txt file.
            extractor.GetText(outputTxtPath);
        }

        Console.WriteLine($"Text extraction completed. Output saved to '{outputTxtPath}'.");
    }
}