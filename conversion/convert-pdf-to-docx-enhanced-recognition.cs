using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOCX save options with enhanced recognition for tables/graphics
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the enhanced flow mode that supports table recognition
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow
                // Additional options can be set here if needed, e.g.:
                // RecognizeBullets = true,
                // RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as DOCX using the specified options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}