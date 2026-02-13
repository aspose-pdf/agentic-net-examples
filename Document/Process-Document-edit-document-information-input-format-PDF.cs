using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Edit document information (metadata)
            pdfDocument.Info.Title = "New Document Title";
            pdfDocument.Info.Author = "John Doe";
            pdfDocument.Info.Subject = "Updated subject of the PDF";
            pdfDocument.Info.Keywords = "Aspose.Pdf, Metadata, Example";

            // Save the modified PDF (using the provided document-save rule)
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}