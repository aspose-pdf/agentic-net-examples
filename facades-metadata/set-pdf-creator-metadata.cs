using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string creator    = "My Custom Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file info, set the Creator property, and save the updated file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Creator = creator;                     // Assign custom Creator value
            bool saved = pdfInfo.SaveNewInfo(outputPath);  // Persist changes to a new file

            if (saved)
                Console.WriteLine($"Creator set and saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to save updated PDF.");
        }
    }
}