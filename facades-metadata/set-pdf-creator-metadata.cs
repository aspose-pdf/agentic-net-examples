using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string creator = "My Custom Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file info facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Assign a custom Creator value
            pdfInfo.Creator = creator;

            // Persist the changes to a new PDF file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Creator set and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}