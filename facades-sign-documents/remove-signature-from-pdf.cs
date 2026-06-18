using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Remove the signature by its name (no SignatureName object needed)
            pdfSign.RemoveSignature("ApprovalSignature");

            // Save the updated PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signature 'ApprovalSignature' removed. Saved to '{outputPath}'.");
    }
}