using System;
using System.IO;
using Aspose.Pdf;
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

        try
        {
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                pdfInfo.SetMetaInfo("ReviewedBy", "John Doe");
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Custom metadata 'ReviewedBy' added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}