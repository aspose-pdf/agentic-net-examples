using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "My New PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Update the Title metadata
        pdfInfo.Title = newTitle;

        // Save the updated PDF to a new file
        bool saved = pdfInfo.SaveNewInfo(outputPath);
        Console.WriteLine(saved
            ? $"Title updated successfully. Saved to '{outputPath}'."
            : "Failed to save the updated PDF.");

        // Release resources
        pdfInfo.Close();
    }
}