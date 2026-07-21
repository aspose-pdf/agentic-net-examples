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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSignature facade
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the signed PDF file
                pdfSign.BindPdf(inputPath);

                // Remove usage rights if present
                if (pdfSign.ContainsUsageRights())
                {
                    pdfSign.RemoveUsageRights();
                }

                // Save the resulting PDF
                pdfSign.Save(outputPath);
            }

            Console.WriteLine($"Usage rights removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}