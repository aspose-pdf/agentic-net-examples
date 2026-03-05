using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output DOC file path
        const string docPath = "output.doc";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOC save options
            var saveOptions = new DocSaveOptions
            {
                // Specify the target format as classic .doc (binary Word format)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Optional: tweak recognition parameters
                RelativeHorizontalProximity = 2.5f,
                RecognizeBullets = true
            };

            // Save the PDF as a DOC file using the specified options
            pdfDocument.Save(docPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{pdfPath}' → '{docPath}'");
    }
}