using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";
        const string outputPath = "signed_without_usage_rights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSignature is a facade class; it does not implement IDisposable.
            PdfFileSignature signature = new PdfFileSignature();

            // Load the signed PDF.
            signature.BindPdf(inputPath);

            // Remove any extended usage rights that were added to the document.
            signature.RemoveUsageRights();

            // Save the modified PDF.
            signature.Save(outputPath);

            Console.WriteLine($"Extended usage rights removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}