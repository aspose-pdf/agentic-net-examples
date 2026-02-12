using System;
using System.IO;
using Aspose.Pdf;

class ExportAccessibility
{
    static void Main(string[] args)
    {
        // Input PDF path (existing PDF with accessibility tags)
        const string inputPath = "input.pdf";

        // Output PDF path (new PDF that will contain the same accessibility data)
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document to a new file.
            // This uses the provided document-save rule: {DocumentVar}.Save({OutputPath});
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Accessibility data exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}