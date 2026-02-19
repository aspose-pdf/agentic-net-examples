using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output DOCX
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

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

            // Configure DOCX save options – only the recognition mode is required.
            // The target format (DOCX) is inferred from the output file extension.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
