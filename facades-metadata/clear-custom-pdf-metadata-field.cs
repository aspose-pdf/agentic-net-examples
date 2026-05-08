using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF metadata facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Remove the custom entry by setting its value to an empty string
            pdfInfo.SetMetaInfo("ObsoleteField", string.Empty);

            // Persist the changes to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Custom metadata 'ObsoleteField' cleared. Saved to '{outputPath}'.");
    }
}