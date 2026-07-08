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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure save options for DOCX conversion
            var saveOptions = new DocSaveOptions
            {
                // Output format: DOCX (correct enum member is DocX)
                Format = DocSaveOptions.DocFormat.DocX,
                // Optional: improve bullet detection (still supported)
                RecognizeBullets = true
                // Note: The Mode/RecognitionMode property has been removed in recent versions.
            };

            // Save the PDF as DOCX with the specified options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}
