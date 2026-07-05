using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure save options to produce a DOC file with plain‑text extraction
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Textbox recognition mode, which extracts text without layout formatting
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOC using the specified options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}