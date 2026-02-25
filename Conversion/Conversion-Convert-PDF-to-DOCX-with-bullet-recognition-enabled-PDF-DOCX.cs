using System;
using System.IO;
using Aspose.Pdf;   // DocSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX save options with bullet recognition enabled
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet detection during conversion
                RecognizeBullets = true
            };

            // Save the document as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX with bullets: {outputPath}");
    }
}