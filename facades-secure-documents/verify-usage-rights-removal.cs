using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the PDF into the signature facade
        PdfFileSignature signature = new PdfFileSignature();
        signature.BindPdf(inputPath);

        // Remove any usage rights present in the document
        signature.RemoveUsageRights();

        // Verify that usage rights have been removed
        bool hasUsageRights = signature.ContainsUsageRights();
        Console.WriteLine("Contains usage rights after removal: " + hasUsageRights);

        // Save the modified PDF
        signature.Save(outputPath);
        Console.WriteLine("Modified PDF saved to " + outputPath);
    }
}