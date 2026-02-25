using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDoc = "output.doc";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOC save options (must be supplied for non‑PDF output)
            DocSaveOptions saveOpts = new DocSaveOptions
            {
                // Explicitly request the legacy .doc format
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Flow recognition mode for maximum editability
                Mode   = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve conversion quality
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOC file using the options above
            pdfDoc.Save(outputDoc, saveOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputDoc}'");
    }
}