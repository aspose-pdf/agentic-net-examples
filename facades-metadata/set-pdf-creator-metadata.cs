using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Copy original PDF so we can modify the copy
        File.Copy(inputPath, outputPath, true);

        // PdfFileInfo implements IDisposable – wrap in using for deterministic cleanup
        using (PdfFileInfo pdfInfo = new PdfFileInfo(outputPath))
        {
            // Assign a custom Creator value
            pdfInfo.Creator = "My Custom Creator";

            // Persist the updated metadata back to the file – SaveNewInfo requires the output file path
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Custom Creator set and saved to '{outputPath}'.");
    }
}
