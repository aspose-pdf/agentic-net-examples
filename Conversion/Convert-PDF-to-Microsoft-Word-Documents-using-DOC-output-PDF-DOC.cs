using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class PdfToDocConverter
{
    static void Main()
    {
        // Paths to the source PDF and the target DOC file.
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as classic .doc (binary Word format).
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for maximum editability.
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet detection during conversion.
                RecognizeBullets = true,

                // Adjust horizontal proximity for word grouping (optional).
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the specified options.
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
    }
}