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

        // Load the PDF file info, modify the Creator property, and save the changes.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Creator = creator;               // Assign custom Creator value
            bool saved = pdfInfo.SaveNewInfo(outputPath); // Persist changes to a new file

            Console.WriteLine(saved
                ? $"Creator set successfully. Saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}