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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document and ensure deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOC save options to extract plain text (use Textbox mode)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Set the output format to DOC
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Textbox recognition mode for simple text extraction
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOC using the specified options
            pdfDocument.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}