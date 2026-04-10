using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newSubject = "Purpose of this document: financial report Q1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF metadata using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Subject property
            pdfInfo.Subject = newSubject;

            // Save the updated PDF to a new file
            bool success = pdfInfo.SaveNewInfo(outputPath);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save updated PDF metadata.");
                return;
            }
        }

        Console.WriteLine($"Subject updated and saved to '{outputPath}'.");
    }
}