using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination DOCX
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX save options with Flow recognition mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Full content analysis for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOCX using the configured options (lifecycle: save)
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX (Flow mode): {outputPath}");
    }
}