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

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            // Use the overload that accepts a signature name as a string.
            pdfSign.RemoveSignature("ApprovalSignature");
            pdfSign.Save(outputPath);
        }

        Console.WriteLine("Signature 'ApprovalSignature' removed. Saved to 'output.pdf'.");
    }
}
