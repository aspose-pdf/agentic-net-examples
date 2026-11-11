using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSignature implements IDisposable, so use a using block
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                // Bind the PDF that contains signatures
                pdfSignature.BindPdf(inputPath);

                // Remove all digital signatures and save a clean copy
                pdfSignature.RemoveSignature(outputPath);
            }

            Console.WriteLine($"All signatures removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signature removal: {ex.Message}");
        }
    }
}
