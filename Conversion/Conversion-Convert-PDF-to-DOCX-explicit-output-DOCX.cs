using System;
using System.IO;
using Aspose.Pdf; // DocSaveOptions resides in this namespace

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output DOCX path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure DOCX save options (DocSaveOptions is in Aspose.Pdf namespace)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Choose DOCX format explicitly
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability (optional)
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
