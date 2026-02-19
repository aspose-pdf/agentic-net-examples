using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output PowerPoint file path
        const string outputPath = "output.pptx";

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

            // Convert and save the document as PPTX using the default conversion options
            pdfDocument.Save(outputPath, SaveFormat.Pptx); // document-save rule with format overload

            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}