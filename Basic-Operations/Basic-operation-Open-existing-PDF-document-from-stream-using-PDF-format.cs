using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the existing PDF file
        const string inputPath = "input.pdf";
        // Path where the PDF will be saved after loading
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open a read‑only stream for the PDF file
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Load the PDF document from the stream
            Document pdfDocument = new Document(inputStream);

            // Example operation: display the number of pages
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

            // Save the document to the specified output path (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Document saved to: {outputPath}");
    }
}