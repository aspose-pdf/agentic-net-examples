using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output DOCX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPdf = args[0];
        string outputDocx = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Configure save options to preserve the original layout
            var saveOptions = new DocSaveOptions
            {
                // Use Textbox mode for maximal layout preservation (limited editability)
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputDocx, saveOptions);

            Console.WriteLine($"Conversion completed successfully. DOCX saved to: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}