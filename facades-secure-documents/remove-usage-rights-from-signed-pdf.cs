using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "signed_without_usage_rights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade for signature operations
            PdfFileSignature pdfSignature = new PdfFileSignature();

            // Bind the existing signed PDF
            pdfSignature.BindPdf(inputPath);

            // Remove usage rights if they exist
            if (pdfSignature.ContainsUsageRights())
            {
                pdfSignature.RemoveUsageRights();
            }

            // Save the modified PDF
            pdfSignature.Save(outputPath);

            // Release resources
            pdfSignature.Close();

            Console.WriteLine($"Usage rights removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}