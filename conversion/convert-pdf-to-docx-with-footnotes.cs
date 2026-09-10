using System;
using System.IO;
using Aspose.Pdf; // Core API namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, convert to DOCX with enhanced flow mode to preserve footnotes
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the most accurate recognition mode (supports footnotes, tables, etc.)
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Optional: improve bullet detection
                RecognizeBullets = true
            };

            // Save the document as DOCX
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: '{outputDocx}'");
    }
}