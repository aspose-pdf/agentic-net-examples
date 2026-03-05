using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired output DOC file path
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure explicit save options for DOC output
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Specify the target format as legacy .doc (binary Word format)
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve bullet detection and spacing handling
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the explicit options
            pdfDocument.Save(outputDocPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}