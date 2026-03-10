using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source document (can be PDF or any supported input format)
        const string inputPath = "input.pdf";
        // Desired output PDF file path
        const string outputPath = "exported_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Save the document as PDF.
                // Document.Save(string) always writes PDF regardless of the file extension,
                // preserving layout, fonts, and formatting.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or saving
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}