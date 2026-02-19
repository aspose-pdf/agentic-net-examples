using System;
using System.IO;
using Aspose.Pdf; // PptxSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPdf = Path.Combine("Data", "sample.pdf");
        // Output PPTX file path
        string outputPptx = Path.Combine("Data", "sample.pptx");

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Create an instance of PptxSaveOptions (default settings)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX using the options instance
            pdfDocument.Save(outputPptx, pptxOptions);

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}