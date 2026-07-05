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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and ensure resources are released
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF document to the facade
            pdfSign.BindPdf(inputPath);

            // Remove the signature with the specified name using the string overload
            pdfSign.RemoveSignature(signatureName);

            // Save the resulting PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signature '{signatureName}' removed. Saved to '{outputPath}'.");
    }
}
