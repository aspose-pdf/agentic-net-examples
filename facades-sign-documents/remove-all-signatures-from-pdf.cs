using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that may contain digital signatures
        const string inputPath  = "input.pdf";
        // Output PDF with all signatures removed
        const string outputPath = "signed_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade for signature operations
        PdfFileSignature pdfSignature = new PdfFileSignature();

        // Bind the source PDF file
        pdfSignature.BindPdf(inputPath);

        // Remove every signature present in the document
        pdfSignature.RemoveSignatures();

        // Save the cleaned PDF to a new file
        pdfSignature.Save(outputPath);

        // Release resources held by the facade
        pdfSignature.Close();

        Console.WriteLine($"All signatures removed. Clean PDF saved to '{outputPath}'.");
    }
}