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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Creator = "My Custom Creator";
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (saved)
            {
                Console.WriteLine("Creator updated and saved to '" + outputPath + "'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
            }
        }
    }
}
