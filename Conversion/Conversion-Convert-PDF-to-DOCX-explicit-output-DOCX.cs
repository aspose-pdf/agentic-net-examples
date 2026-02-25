using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX save options (must be passed explicitly)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional but common)
                RecognizeBullets = true
            };

            // Save the document as DOCX
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
    }
}