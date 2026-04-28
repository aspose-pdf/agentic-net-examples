using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output DOC file path
        const string outputDoc = "output.doc";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document, convert and save as DOC using a custom recognition mode
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOC (not DOCX)
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Textbox recognition mode – fast, preserves layout,
                // and keeps images as images without extensive text flow analysis.
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOC with the specified options
            pdfDocument.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}