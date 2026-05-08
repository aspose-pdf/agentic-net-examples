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
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure save options for DOCX conversion
            var saveOptions = new DocSaveOptions
            {
                // Use EnhancedFlow mode for better recognition of footnotes, tables, etc.
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Enable bullet detection (optional, improves list handling)
                RecognizeBullets = true
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}
