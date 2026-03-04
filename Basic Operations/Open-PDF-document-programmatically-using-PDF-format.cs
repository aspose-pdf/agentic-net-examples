using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to open
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document using a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Output basic information about the opened document
            Console.WriteLine($"Pages: {pdfDoc.Pages.Count}");
            Console.WriteLine($"Title: {pdfDoc.Info.Title}");
            Console.WriteLine($"Author: {pdfDoc.Info.Author}");

            // Example: save a copy of the opened document (optional)
            const string outputPath = "copy_of_sample.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Document saved as: {outputPath}");
        }
    }
}