using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string signatureName = "ApprovalSignature";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSignature facade to manipulate signatures
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the PDF document
            pdfSign.BindPdf(inputPath);

            // Remove the specified signature directly by its name
            pdfSign.RemoveSignature(signatureName);

            // Save the modified PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signature '{signatureName}' removed. Output saved to '{outputPath}'.");
    }
}