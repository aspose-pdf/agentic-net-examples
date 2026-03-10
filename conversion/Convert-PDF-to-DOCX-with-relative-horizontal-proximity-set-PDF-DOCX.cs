using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Set relative horizontal proximity (example value)
                RelativeHorizontalProximity = 2.5f,
                // Enable bullet recognition (optional)
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using explicit save options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}