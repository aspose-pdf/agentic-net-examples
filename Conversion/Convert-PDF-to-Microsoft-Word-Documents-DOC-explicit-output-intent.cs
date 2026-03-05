using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format as classic .doc (binary Word format).
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for maximum editability.
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet detection and adjust horizontal proximity.
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the explicit save options.
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}