using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Create a PdfFileInfo facade and bind it to the existing PDF
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);

        // Set the desired metadata properties
        pdfInfo.Title = "Sample PDF Title";
        pdfInfo.Author = "John Doe";
        pdfInfo.Subject = "Demonstration of metadata setting using Aspose.Pdf.Facades";
        pdfInfo.Keywords = "Aspose.Pdf, Metadata, C#";

        // Save the PDF with the updated metadata
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata successfully updated. Saved to: {outputPath}");
    }
}