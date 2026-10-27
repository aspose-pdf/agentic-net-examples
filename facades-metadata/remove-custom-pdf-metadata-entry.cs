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

        try
        {
            // Load the PDF using the Facades API
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPath);

            // Clear the custom metadata entry "ObsoleteField"
            pdfInfo.SetMetaInfo("ObsoleteField", string.Empty);

            // Save the updated PDF
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata 'ObsoleteField' cleared. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}