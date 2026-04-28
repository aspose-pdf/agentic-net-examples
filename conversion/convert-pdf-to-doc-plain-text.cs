using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPdf = "input.pdf";
        // Output DOC file path
        string outputDoc = "output.doc";

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
                // Specify DOC format
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Textbox recognition mode (closest to plain‑text extraction)
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOC using the specified options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}