using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Edit document metadata
        pdfDocument.Info.Title = "Updated Document Title";
        pdfDocument.Info.Author = "Jane Smith";
        pdfDocument.Info.Subject = "Demonstration of editing PDF properties";
        pdfDocument.Info.Keywords = "Aspose.Pdf, metadata, example";

        // Save the modified PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);
    }
}