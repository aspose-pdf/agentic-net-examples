using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Desired output DOCX file path
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options (non‑PDF format requires explicit options)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional but common)
                RecognizeBullets = true
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}