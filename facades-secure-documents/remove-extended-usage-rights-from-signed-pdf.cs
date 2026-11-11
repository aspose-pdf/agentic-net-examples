using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "signed_without_usage_rights.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfFileSignature facade to work with signatures and usage rights
            PdfFileSignature pdfSignature = new PdfFileSignature();

            // Load the signed PDF
            pdfSignature.BindPdf(inputPath);

            // Remove any extended usage rights that may be present
            pdfSignature.RemoveUsageRights();

            // Save the modified PDF (usage rights removed)
            pdfSignature.Save(outputPath);

            Console.WriteLine($"Usage rights removed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}