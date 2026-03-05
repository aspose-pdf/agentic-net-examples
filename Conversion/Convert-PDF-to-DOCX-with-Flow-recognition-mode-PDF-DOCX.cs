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

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX save options with Flow recognition mode
            var saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Full content analysis for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition (common for DOCX)
                RecognizeBullets = true
            };

            // Save the document as DOCX (lifecycle: save)
            pdfDoc.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}