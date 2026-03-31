using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Title = "Updated PDF Title";

            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (saved)
            {
                Console.WriteLine("Title updated and saved to '" + outputPath + "'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
            }
        }
    }
}