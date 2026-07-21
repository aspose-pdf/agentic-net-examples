using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF to load
        const string outputPath = "output.pdf"; // where to save after loading

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the Document class (PdfFileEditor does not support BindPdf/Save/Close).
            Document pdfDoc = new Document(inputPath);

            // Optional: perform operations on pdfDoc here (e.g., add stamps, delete pages, etc.).

            // Save the (potentially modified) PDF to a new file.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF loaded and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
