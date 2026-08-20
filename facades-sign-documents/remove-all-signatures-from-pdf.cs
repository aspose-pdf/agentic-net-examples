using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade, bind the PDF, remove all signatures, and save the result
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);          // Load the PDF
            pdfSignature.RemoveSignatures();          // Remove all digital signatures
            pdfSignature.Save(outputPath);            // Save the cleaned PDF
        }

        Console.WriteLine($"All signatures removed. Clean PDF saved to '{outputPath}'.");
    }
}