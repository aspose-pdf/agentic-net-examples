using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including DocSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output DOC file path
        const string outputDoc = "output.doc";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as DOC
                Format = DocSaveOptions.DocFormat.Doc,
                // Set the recognition mode to a plain‑text oriented mode
                // (Textbox mode provides a fast conversion preserving text content)
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the PDF as a DOC file using the configured options
            pdfDocument.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}