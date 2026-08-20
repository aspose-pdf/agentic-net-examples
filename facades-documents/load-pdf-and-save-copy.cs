using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // source PDF file
        const string outputPath = "output_copy.pdf"; // destination (optional)

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not support BindPdf/Save/Close)
        Document pdfDoc = new Document(inputPath);

        // Example operation: save a copy of the loaded PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF loaded from '{inputPath}' and saved to '{outputPath}'.");
    }
}
