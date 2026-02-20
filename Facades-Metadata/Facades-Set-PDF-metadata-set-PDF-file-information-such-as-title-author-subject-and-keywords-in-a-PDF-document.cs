using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Create a PdfFileInfo object and bind it to the existing PDF
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath); // Load the PDF

        // Set the desired metadata properties
        pdfInfo.Title = "Sample PDF Title";
        pdfInfo.Author = "John Doe";
        pdfInfo.Subject = "Demonstration of metadata setting";
        pdfInfo.Keywords = "Aspose.Pdf, metadata, example";

        // Save the updated PDF to a new file
        pdfInfo.Save(outputPath); // Save the document

        Console.WriteLine($"Metadata updated successfully. Saved to '{outputPath}'.");
    }
}