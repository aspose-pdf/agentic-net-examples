using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output DOC file path (extension determines format)
        const string outputPath = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document as DOC (format inferred from the .doc extension)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Conversion successful: '{outputPath}' created.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}