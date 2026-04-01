using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            // Use the overload that accepts a signature name as a string.
            pdfSign.RemoveSignature("ApprovalSignature");
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signature removed and saved to '{outputPath}'.");
    }
}