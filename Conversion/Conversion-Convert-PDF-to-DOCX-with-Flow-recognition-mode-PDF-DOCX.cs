using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including DocSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options with Flow recognition mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the desired output format (DOCX)
                Format = DocSaveOptions.DocFormat.DocX,
                // Use full Flow recognition for maximum editability
                Mode   = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition
                RecognizeBullets = true
            };

            // Save the PDF as a DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}