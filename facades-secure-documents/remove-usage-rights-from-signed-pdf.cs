using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "usage_rights_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileSignature facade
            PdfFileSignature pdfSignature = new PdfFileSignature();

            // Load the signed PDF
            pdfSignature.BindPdf(inputPath);

            // Optional: check if usage rights are present
            if (pdfSignature.ContainsUsageRights())
            {
                // Remove the usage rights entry
                pdfSignature.RemoveUsageRights();
                Console.WriteLine("Usage rights removed.");
            }
            else
            {
                Console.WriteLine("No usage rights found in the document.");
            }

            // Save the modified PDF
            pdfSignature.Save(outputPath);
            pdfSignature.Close(); // Release resources

            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}