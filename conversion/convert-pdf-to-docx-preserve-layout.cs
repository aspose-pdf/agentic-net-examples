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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure save options for DOCX conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Preserve the original layout (standard/text‑box mode)
                Mode   = DocSaveOptions.RecognitionMode.Textbox,
                // Specify the desired output format
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX using the explicit save options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}