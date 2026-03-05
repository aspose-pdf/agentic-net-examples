using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes Document and DocSaveOptions

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the target DOC file
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Ensure the source file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options – required when saving to a non‑PDF format
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Specify the output format (.doc)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Optional: improve bullet detection and paragraph grouping
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a Word document using the configured options
            pdfDocument.Save(outputDocPath, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
    }
}