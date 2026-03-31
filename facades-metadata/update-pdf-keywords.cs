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

        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfStream))
        {
            // Update the Keywords metadata
            pdfInfo.Keywords = "Sample, Aspose, PDF";

            // Save the PDF with the updated information
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}