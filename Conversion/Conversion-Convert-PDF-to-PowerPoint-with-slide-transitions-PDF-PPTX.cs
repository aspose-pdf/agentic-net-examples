using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxWithTransitions
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output PPTX path (second argument)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToPptxWithTransitions <input.pdf> <output.pptx>");
            return;
        }

        string pdfPath = args[0];
        string pptxPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Convert and save the PDF as PPTX (Aspose.Pdf can directly save to PPTX)
            pdfDocument.Save(pptxPath, SaveFormat.Pptx);
            Console.WriteLine($"PDF successfully converted to PPTX at '{pptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
