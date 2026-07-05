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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the signed PDF
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Remove usage rights if they are present
        if (pdfSignature.ContainsUsageRights())
        {
            pdfSignature.RemoveUsageRights();
        }

        // Save the modified PDF
        pdfSignature.Save(outputPath);
        pdfSignature.Close();

        Console.WriteLine($"Usage rights removed. Output saved to '{outputPath}'.");
    }
}