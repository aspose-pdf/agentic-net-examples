using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX save options as required
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Set the output format to DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the enhanced flow recognition mode
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Enable bullet list recognition
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}