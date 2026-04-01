using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "usage_rights_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPath);

            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            if (hasUsageRights)
            {
                pdfSignature.RemoveUsageRights();
                Console.WriteLine("Extended usage rights removed.");
            }
            else
            {
                Console.WriteLine("No extended usage rights present.");
            }

            pdfSignature.Save(outputPath);
            Console.WriteLine("Result saved to '" + outputPath + "'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}