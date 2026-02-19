using System;
using System.IO;
using Aspose.Pdf; // DocSaveOptions resides directly in this namespace

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

        string inputPdfPath = args[0];
        string outputDocxPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure save options for DOCX conversion
            var saveOptions = new DocSaveOptions
            {
                // Flow mode provides maximum editability of the resulting Word document.
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Specify that the output should be a DOCX file.
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}