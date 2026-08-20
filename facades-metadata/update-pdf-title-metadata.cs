using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "My Updated PDF Title";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF metadata facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Title property
            pdfInfo.Title = newTitle;

            // Save the PDF with the updated metadata
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Title updated and saved to '{outputPath}'."
                : $"Failed to save updated PDF to '{outputPath}'.");
        }
    }
}