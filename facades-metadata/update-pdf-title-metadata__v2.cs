using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "My Updated PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information using the Facades API.
        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Update the Title metadata property.
            fileInfo.Title = newTitle;

            // Save the updated metadata to a new file without re‑saving the whole PDF.
            fileInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Title updated and saved to '{outputPath}'.");
    }
}