using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "usage_rights_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the signed PDF
            pdfSign.BindPdf(inputPath);

            // Remove usage rights if present
            if (pdfSign.ContainsUsageRights())
            {
                pdfSign.RemoveUsageRights();
                Console.WriteLine("Extended usage rights removed.");
            }
            else
            {
                Console.WriteLine("No extended usage rights found.");
            }

            // Save the resulting PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Processed file saved as '{outputPath}'.");
    }
}