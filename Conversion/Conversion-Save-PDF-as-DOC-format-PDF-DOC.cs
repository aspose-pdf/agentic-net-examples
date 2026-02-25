using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including DocSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options (required for non‑PDF output)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format (.doc)
                Format = DocSaveOptions.DocFormat.Doc,
                // Choose a recognition mode; Flow provides editable text
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition for better list handling
                RecognizeBullets = true
            };

            // Save the PDF as a DOC file using the specified options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: '{outputDocPath}'");
    }
}