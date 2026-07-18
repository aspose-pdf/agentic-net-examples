using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set the custom metadata entry "ObsoleteField" to an empty value
            pdfInfo.SetMetaInfo("ObsoleteField", string.Empty);

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Custom metadata cleared. Saved to '{outputPath}'.");
    }
}