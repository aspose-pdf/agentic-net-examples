using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update metadata fields
            pdfInfo.Title = "New Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demo of SaveNewInfo";
            pdfInfo.Keywords = "Aspose, PDF, metadata";

            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved ? $"Metadata saved to '{outputPath}'." : "Failed to save metadata.");
        }
    }
}