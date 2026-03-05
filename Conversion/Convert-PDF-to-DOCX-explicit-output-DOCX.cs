using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            var saveOptions = new DocSaveOptions
            {
                // Specify DOCX format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow recognition mode for editable output
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve bullet detection
                RecognizeBullets = true
            };

            // Save as DOCX – must pass explicit SaveOptions for non‑PDF formats
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}
