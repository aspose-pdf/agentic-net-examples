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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options for Enhanced Flow recognition
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Enhanced Flow mode (full recognition with table support)
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Optional settings to improve editability
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as DOCX using explicit save options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}