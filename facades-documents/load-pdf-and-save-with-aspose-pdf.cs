using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf/Save/Close).
        Document pdfDoc = new Document(inputPath);

        // Perform any desired editing here (e.g., add stamps, delete pages, etc.).
        // For this example we simply save the loaded document unchanged.

        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF loaded from '{inputPath}' and saved to '{outputPath}'.");
    }
}
