using System;
using System.IO;
using Aspose.Pdf;               // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Desired output DOC file path
        const string outputDoc = "output.doc";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document, process, and save as DOC
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOC save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format (.doc)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOC using the explicit save options
            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}