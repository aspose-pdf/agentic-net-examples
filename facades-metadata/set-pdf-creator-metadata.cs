using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf"; // file where updated metadata will be saved

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata without loading the full document
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Assign a custom Creator value
        pdfInfo.Creator = "My Custom Creator";

        // Persist the updated metadata back to a PDF file (outputPath is required)
        pdfInfo.SaveNewInfo(outputPath);

        Console.WriteLine("Creator value has been updated successfully.");
    }
}
