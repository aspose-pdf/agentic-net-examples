using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output DOC/DOCX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocConverter <input.pdf> <output.docx>");
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
            // Load the PDF document (load rule)
            Document pdfDocument = new Document(inputPath);

            // Configure DOC save options with Flow recognition mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Full recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Choose DOCX output format (can be Doc for legacy .doc)
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOC/DOCX (save rule)
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
