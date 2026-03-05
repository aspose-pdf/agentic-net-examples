using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options (required when saving to a non‑PDF format)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional, improves conversion of lists)
                RecognizeBullets = true
            };

            // Save the PDF as a DOCX file using the configured options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}