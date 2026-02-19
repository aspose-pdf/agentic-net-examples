using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output DOCX path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Configure conversion options.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Choose a recognition mode. Flow provides better editability.
                Mode = DocSaveOptions.RecognitionMode.Flow
                // No need to set DocFormat here – the format is inferred from the
                // file extension (or you can use the overload that takes SaveFormat).
            };

            // Save the document as DOCX using the configured options.
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
