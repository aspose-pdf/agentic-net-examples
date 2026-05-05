using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo();

        // Bind the existing PDF document
        pdfInfo.BindPdf(inputPath);

        // Set XMP metadata fields
        pdfInfo.Author = "John Doe";
        pdfInfo.Title  = "Project Plan";

        // Save the updated PDF with the new XMP metadata
        bool saved = pdfInfo.SaveNewInfoWithXmp(outputPath);
        Console.WriteLine(saved
            ? $"XMP metadata added. Saved to '{outputPath}'."
            : "Failed to save PDF with updated XMP metadata.");

        // Release resources
        pdfInfo.Close();
    }
}