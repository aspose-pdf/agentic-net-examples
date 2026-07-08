using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the cleaned output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_removed.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileSignature facade
            PdfFileSignature pdfSignature = new PdfFileSignature();

            // Bind the existing PDF file for editing
            pdfSignature.BindPdf(inputPath);

            // Remove all digital signatures from the document
            pdfSignature.RemoveSignatures();

            // Save the cleaned PDF to a new file
            pdfSignature.Save(outputPath);

            Console.WriteLine($"All signatures removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}