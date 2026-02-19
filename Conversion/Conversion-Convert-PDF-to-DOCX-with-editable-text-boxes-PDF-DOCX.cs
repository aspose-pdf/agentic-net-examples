using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output DOCX file path
        const string outputPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure save options to produce a DOCX with editable text boxes
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the Textbox recognition mode for fast conversion preserving layout
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Specify DOCX as the output format
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}