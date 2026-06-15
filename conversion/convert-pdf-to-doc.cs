using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including DocSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options – only the format is required for a basic conversion
            DocSaveOptions docOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.Doc
                // All other settings remain at their defaults (default recognition mode, etc.)
            };

            // Save as DOC using the explicit SaveOptions (required for non‑PDF output)
            pdfDocument.Save(outputDocPath, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
    }
}