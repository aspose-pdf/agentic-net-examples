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

        // Initialize the PdfFileInfo facade and bind the PDF
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);

        // Remove the custom metadata entry by setting its value to empty
        pdfInfo.SetMetaInfo("ObsoleteField", string.Empty);

        // Save the PDF with the updated metadata
        pdfInfo.SaveNewInfo(outputPath);

        // Clean up the facade
        pdfInfo.Close();

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}