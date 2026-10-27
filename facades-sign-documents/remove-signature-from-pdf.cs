using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";
        const string signatureName = "ApprovalSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSignature implements IDisposable; ensure deterministic disposal
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                // Load the PDF document
                pdfSignature.BindPdf(inputPath);

                // Remove the signature with the specified name
                pdfSignature.RemoveSignature(signatureName);

                // Save the updated PDF
                pdfSignature.Save(outputPath);
            }

            Console.WriteLine($"Signature '{signatureName}' removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
