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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Set the Keywords metadata field and save the updated PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Keywords = "Aspose, PDF, Keywords";
            pdfInfo.SaveNewInfo(outputPath);
        }

        // Verify that the Keywords were saved correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPath))
        {
            string keywords = verifyInfo.Keywords;
            if (string.IsNullOrEmpty(keywords))
            {
                Console.WriteLine("FAIL: Keywords not set.");
            }
            else
            {
                Console.WriteLine($"PASS: Keywords = '{keywords}'");
            }
        }
    }
}