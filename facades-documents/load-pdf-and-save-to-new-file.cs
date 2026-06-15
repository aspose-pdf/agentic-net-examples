using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not support BindPdf/Save/Close)
        Document pdfDoc = new Document(inputPath);

        // Perform any desired operations here.
        // For demonstration, simply save the loaded document to a new file.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF loaded and saved to '{outputPath}'.");
    }
}