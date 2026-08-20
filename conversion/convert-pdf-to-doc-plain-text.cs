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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOC
                Format = DocSaveOptions.DocFormat.Doc,
                // Recognition mode – Textbox is the closest to plain‑text extraction
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Insert line breaks to improve readability
                AddReturnToLineEnd = true
            };

            // Save the document as DOC using the specified options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}