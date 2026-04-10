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

        // Bind the PDF, remove the specified signature, and save the result.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Remove the signature by its name using the string overload.
            pdfSign.RemoveSignature("ApprovalSignature");

            // Save the modified PDF.
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signature 'ApprovalSignature' removed. Saved to '{outputPath}'.");
    }
}