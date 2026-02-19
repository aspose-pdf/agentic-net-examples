using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document using Aspose.Pdf.Document
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as PPTX. Aspose.Pdf supports direct PPTX export via SaveFormat.
            pdfDocument.Save(outputPptxPath, SaveFormat.Pptx);

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}