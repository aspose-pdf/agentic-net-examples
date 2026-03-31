using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);
        // Set the custom metadata entry "ObsoleteField" to an empty value, effectively removing it
        pdfInfo.SetMetaInfo("ObsoleteField", "");
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata entry 'ObsoleteField' cleared and saved to '{outputPath}'.");
    }
}