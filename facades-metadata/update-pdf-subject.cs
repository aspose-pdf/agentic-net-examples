using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newSubject = "Report on Quarterly Sales";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Subject = newSubject;
            bool success = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(success ? "Subject updated and saved to '" + outputPath + "'." : "Failed to save updated PDF.");
        }
    }
}